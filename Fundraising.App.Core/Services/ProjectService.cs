using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundraising.App.Core.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IApplicationDbContext _dbContext;

        public ProjectService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                CreatorId = optionProject.CreatorId
            };

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return new OptionsProject(project);
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

        // READ / ALL
        // --------------------------------------------------------
        public List<OptionsProject> GetAllProjects()
        {
            List<Project> projects = _dbContext.Projects.ToList();
            List<OptionsProject> optionsProject = new();
            projects.ForEach(project => optionsProject.Add(new OptionsProject(project)));
            return optionsProject;
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

