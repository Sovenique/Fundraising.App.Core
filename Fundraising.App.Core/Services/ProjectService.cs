using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundraising.App.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly FundraisingAppDbContext dbContext = new FundraisingAppDbContext();
        private object project;
        
        public OptionsProject CreateProject(OptionsProject optionProjrct)
        {
            Project project = new()
            {
                Title = optionProjrct.Title,
                Description = optionProjrct.Description,
                Category = optionProjrct.Category,
                ProjectStatus = optionProjrct.ProjectStatus,
                Creator = optionProjrct.Creator,
                CreatedDate = DateTime.Now,
                AmountGathered = 0,//check 0
                TargetAmount = optionProjrct.TargetAmount,//check if its necessary
                Rewards = optionProjrct.Rewards,//check if its necessary
            };
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();
            return new OptionsProject
            {
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                ProjectStatus = project.ProjectStatus,
                Creator = project.Creator,
                TargetAmount = project.TargetAmount,
                Rewards = project.Rewards
            };
        }

        public bool DeleteProject(int Id)
        {
            Project dbContextProject = dbContext.Projects.Find(Id);
            if (dbContextProject == null) return false;
            dbContext.Projects.Remove(dbContextProject);
            return true;//message

        }

        public List<OptionsProject> GetAllProjects()
        {
            List<Project> projects = dbContext.Projects.ToList();
            List<OptionsProject> optionsProject = new();
            projects.ForEach(project => optionsProject.Add(new OptionsProject()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category,
                ProjectStatus = project.ProjectStatus,
                Creator = project.Creator,
                TargetAmount = project.TargetAmount,
                Rewards = project.Rewards,
                AmountGathered = project.AmountGathered//check if its necessery

            }));
            return optionsProject;
        }

        public OptionsProject GetOptionsProjectById(int Id)
        {
            Project project = dbContext.Find(Id);
            if (project == null)
            {
                return null;//message
            }
            return new OptionsProject(project);
        }

        public OptionsProject UpdateProject(OptionsProject optionsProject, int Id)
        {
            Project dbContextProject = dbContext.Projects.Find(Id);
            if (dbContextProject == null) return null;
            dbContextProject.Title = optionsProject.Title;

            dbContext.SaveChanges();
            return new OptionsProject(dbContextProject);




        }
    }
}
