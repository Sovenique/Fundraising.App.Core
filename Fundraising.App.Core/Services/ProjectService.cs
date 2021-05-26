using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IApplicationDbContext dbContext, ILogger<ProjectService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<Project>> CreateProjectAsync(OptionsProject optionProject)
        {
            if (optionProject == null)
            {
                return new Result<Project>(ErrorCode.BadRequest, "Null option.");
            }
            if (string.IsNullOrWhiteSpace(optionProject.Title) ||
                string.IsNullOrWhiteSpace(optionProject.Description) ||
                string.IsNullOrWhiteSpace(optionProject.Category) ||
                string.IsNullOrWhiteSpace(optionProject.Creator.ToString()))//check with team!!
            {
                return new Result<Project>(ErrorCode.BadRequest, "Not all required project options provided.");
            }
            var projectWithSameTitle = await _dbContext.Projects.SingleOrDefaultAsync(cus => cus.Title == optionProject.Title);
            if (projectWithSameTitle == null)
            {
                return new Result<Project>(ErrorCode.Conflict, $"Project with Title :{optionProject.Title} already exists.");
            }

            var newProject = new Project
            {
                Title = optionProject.Title,
                Description = optionProject.Description,
                Category = optionProject.Category,
                ProjectStatus = optionProject.ProjectStatus,
                Creator = optionProject.Creator,
                CreatedDate = DateTime.Now,
                AmountGathered = 0,//check 0
                TargetAmount = optionProject.TargetAmount,//check if its necessary
                Rewards = optionProject.Rewards,//check if its necessary
            };
            await _dbContext.Projects.AddAsync(newProject);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Result<Project>(ErrorCode.InternalServerError, "Could not save reward.");
            }
            return new Result<Project>
            {
                Data = newProject
            };


        }

        public async Task<Result<int>> DeleteProjectByIdAsync(int Id)
        {
            var projectToDelete = await GetProjectByIdAsync(Id);
            if (projectToDelete.Error != null || projectToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Project with id #{Id} not found.");
            }
            _dbContext.Projects.Remove(projectToDelete.Data);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Result<int>(ErrorCode.InternalServerError, $"Could not delete project with id #{Id}.");
            }
            return new Result<int>
            {
                Data = Id
            };

        }

        public async Task<Result<List<Project>>> GetAllProjectsAsync()
        {
            var projects = await _dbContext.Projects.ToListAsync();

            return new Result<List<Project>>
            {
                Data = projects.Count > 0 ? projects : new List<Project>()
            };
        }

        public async Task<Result<Project>> GetProjectByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return new Result<Project>(ErrorCode.BadRequest, "Invalid ID.");
            }
            var project = await _dbContext
                .Projects
                .SingleOrDefaultAsync(cus => cus.Id == Id);
            if (project == null)
            {
                return new Result<Project>(ErrorCode.NotFound, $"Project with id : #{Id} not found");
            }
            return new Result<Project>
            {
                Data = project
            };
        }




        public async Task<Result<Project>> UpdateProjectByIdAsync(OptionsProject optionsProject, int Id)
        {
            if (optionsProject == null)
            {
                return new Result<Project>(ErrorCode.BadRequest, "Null options.");
            }
            var project = await _dbContext
                .Projects
                .SingleOrDefaultAsync(cus => cus.Id == Id);
            if (project == null)
            {
                return new Result<Project>(ErrorCode.NotFound, $"Project with id #{Id} not found.");
            }
            project.Title = optionsProject.Title;
            project.Description = optionsProject.Description;
            project.Category = optionsProject.Category;
            project.ProjectStatus = optionsProject.ProjectStatus;
            project.Creator = optionsProject.Creator;
            project.CreatedDate = DateTime.Now;
            project.AmountGathered = optionsProject.AmountGathered;
            project.TargetAmount = optionsProject.TargetAmount;
            project.Rewards = optionsProject.Rewards;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Result<Project>(ErrorCode.InternalServerError, $"Could not update project with id #{Id}.");
            }
            return new Result<Project>
            {
                Data = project
            };

        }
    }
}

