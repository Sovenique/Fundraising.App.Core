using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
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

        // CREATE
        // --------------------------------------------------------
        public OptionsProject CreateProject(OptionsProject optionProject)
        {


            Project project = new()
            {
                Title = optionProject.Title,
                Description = optionProject.Description,
                CreatedDate = DateTime.Now,
                Category = optionProject.Category,
                TargetAmount = optionProject.TargetAmount,
                CreatorId = optionProject.CreatorId,
                ImagePath = optionProject.ImagePath
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return new OptionsProject(project);
        }
        public async Task<Result<OptionsProject>> CreateProjectAsync(OptionsProject optionsProject)
        {
            if (optionsProject == null)
            {
                return new Result<OptionsProject>(ErrorCode.BadRequest, "Null options.");
            }
            if (string.IsNullOrWhiteSpace(optionsProject.Creator.ToString()) ||
               string.IsNullOrWhiteSpace(optionsProject.Description) ||
               string.IsNullOrWhiteSpace(optionsProject.AmountGathered.ToString()) ||
               string.IsNullOrWhiteSpace(optionsProject.TargetAmount.ToString()) ||
               string.IsNullOrWhiteSpace(optionsProject.Category.ToString()))
            {
                return new Result<OptionsProject>(ErrorCode.BadRequest, "Not all required project options provided.");
            }
            if (optionsProject.TargetAmount >= 0)
            {
                return new Result<OptionsProject>(ErrorCode.BadRequest, "Invalid Target Amount number.");
            }
            var newProject = new Project
            {
                Title = optionsProject.Title,
                Description = optionsProject.Description,
                CreatedDate = DateTime.Now,
                Category = optionsProject.Category,
                TargetAmount = optionsProject.TargetAmount,
                CreatorId = optionsProject.CreatorId,
                ImagePath = optionsProject.ImagePath
            };
            await _dbContext.Projects.AddAsync(newProject);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<OptionsProject>(ErrorCode.InternalServerError, "Could not save project.");
            }
            return new Result<OptionsProject>
            {
                Data = new OptionsProject(newProject)
            };
        }

        // DELETE
        // --------------------------------------------------------
        public bool DeleteProject(int Id)
        {
            Project dbContextProject = _dbContext.Projects.Find(Id);
            if (dbContextProject == null) return false;
            _dbContext.Projects.Remove(dbContextProject);
            _dbContext.SaveChanges();
            return true;

        }

        public async Task<Result<int>> DeleteProjectAsync(int id)
        {
            var projectToDelete = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Id == id);
            if (projectToDelete == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Project with id #{id} not found.");
            }
            _dbContext.Projects.Remove(projectToDelete);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<int>(ErrorCode.InternalServerError, "Could not delete project.");
            }
            return new Result<int>
            {
                Data = id
            };
        }


        // READ / ALL
        // --------------------------------------------------------
        public List<OptionsProject> GetAllProjects()
        {
            List<Project> projects = _dbContext.Projects.ToList();
            List<OptionsProject> optionsProject = new();
            projects.ForEach(project => optionsProject.Add(new OptionsProject(project)));
            return optionsProject;
        }

        public async Task<Result<List<OptionsProject>>> GetAllProjectsAsync()
        {
            var projects = await _dbContext.Projects.ToListAsync();
            List<OptionsProject> optionsProjects = new();

            projects.ForEach(project =>
                optionsProjects.Add(new OptionsProject(project))
            );
            return new Result<List<OptionsProject>>
            {
                Data = optionsProjects.Count > 0 ? optionsProjects : new List<OptionsProject>()
            };
        }

        // READ / BY ID
        // --------------------------------------------------------
        public OptionsProject GetProjectById(int Id)
        {
            Project project = _dbContext.Projects.Find(Id);
            if (project == null)
            {
                return null;
            }
            return new OptionsProject(project);
        }
        public async Task<Result<OptionsProject>> GetProjectByIdAsync(int id)
        {
            if (id <0)
            {
                return new Result<OptionsProject>(ErrorCode.BadRequest, "CreatorId cannot be null.");
            }
            var project = await _dbContext
               .Projects
               .SingleOrDefaultAsync(pro => pro.Id == id);
            if (project == null)
            {
                return new Result<OptionsProject>(ErrorCode.NotFound, $"Product with CreatorId #{id} not found.");
            }
            return new Result<OptionsProject>
            {
                Data = new OptionsProject(project)
            };
        }

        // READ / BY CREATOR ID
        // --------------------------------------------------------
        public List<OptionsProject> GetProjectByCreatorId(string CreatorId)
        {
            List<OptionsProject> optionProjects = new();
            var projects = _dbContext.Projects.Where(project => project.CreatorId == CreatorId).ToList();
            projects.ForEach(project =>
                optionProjects.Add(new OptionsProject(project))
            );

            return optionProjects;

        }

        public async Task<Result<OptionsProject>> GetProjectByCreatorIdAsync(string CreatorId)
        {
            if (CreatorId == null)
            {
                return new Result<OptionsProject>(ErrorCode.BadRequest, "CreatorId cannot be null.");
            }
            var project = await _dbContext
               .Projects
               .SingleOrDefaultAsync(pro => pro.CreatorId == CreatorId);
            if (project == null)
            {
                return new Result<OptionsProject>(ErrorCode.NotFound, $"Product with CreatorId #{CreatorId} not found.");
            }
            return new Result<OptionsProject>
            {
                Data = new OptionsProject(project)
            };
        }

        // UPDATE
        // --------------------------------------------------------
        public OptionsProject UpdateProject(OptionsProject optionsProject, int Id)
        {
            Project dbContextProject = _dbContext.Projects.Find(Id);
            if (dbContextProject == null) return null;

            dbContextProject.Title = optionsProject.Title;
            dbContextProject.Description = optionsProject.Description;
            dbContextProject.AmountGathered = optionsProject.AmountGathered;
            dbContextProject.TargetAmount = optionsProject.TargetAmount;
            dbContextProject.Creator = optionsProject.Creator;


            _dbContext.SaveChanges();
            return new OptionsProject(dbContextProject);

        }
        public async Task<Result<OptionsProject>> UpdateProjectAsync(OptionsProject optionsProject , int id)
        {
            var projectToUpdate = await _dbContext.Projects.SingleOrDefaultAsync(proj => proj.Id == id);
            if ( projectToUpdate == null)
            {
                return new Result<OptionsProject>(ErrorCode.NotFound, $"Project with id #{id} not found.");
            }
            projectToUpdate.Title = optionsProject.Title;
            projectToUpdate.Creator = optionsProject.Creator;
            projectToUpdate.Description = optionsProject.Description;
            projectToUpdate.AmountGathered = optionsProject.AmountGathered;
            projectToUpdate.Category = optionsProject.Category;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<OptionsProject>(ErrorCode.InternalServerError, "Could not save project.");
            }
            return new Result<OptionsProject>
            {
                Data = new OptionsProject(projectToUpdate)
            };

        }

        public OptionsProject UpdateProjectAmount(OptionsProject optionsProject, int Id)
        {
            Project dbContextProject = _dbContext.Projects.Find(Id);
            if (dbContextProject == null) return null;

            dbContextProject.AmountGathered = optionsProject.AmountGathered;
           
            _dbContext.SaveChanges();
            return new OptionsProject(dbContextProject);

        }
    }
}

