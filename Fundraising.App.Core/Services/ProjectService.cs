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
        private readonly FundraisingAppDbContext dbContext;
        private object project;

        public ProjectService(FundraisingAppDbContext _dbConext)
        {
            dbContext = _dbConext;
        }
        public OptionsProject CreateProject(OptionsProject optionProject)
        {
            Project project = new()//If .... ?
            {
                Title = optionProject.Title,
                Description = optionProject.Description,
                Category = optionProject.Category,
                ProjectStatus = optionProject.ProjectStatus,
                Creator = optionProject.Creator,
                CreatedDate = DateTime.Now,
                TargetAmount = optionProject.TargetAmount,
                Rewards = optionProject.Rewards,//check if its necessary .. ???
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
            projects.ForEach(project => optionsProject.Add(new OptionsProject(project)));
            return optionsProject;
        }

        public OptionsProject GetOptionsProjectById(int Id)
        {
            Project project = dbContext.Projects.Find(Id);
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
            dbContextProject.Description = optionsProject.Description;
            dbContextProject.Category = optionsProject.Category;
            dbContextProject.ProjectStatus = optionsProject.ProjectStatus;
            dbContextProject.Creator = optionsProject.Creator;
            dbContextProject.CreatedDate = optionsProject.CreatedDate;
            dbContextProject.AmountGathered = optionsProject.AmountGathered;
            dbContextProject.TargetAmount = optionsProject.TargetAmount;
            dbContextProject.Rewards = optionsProject.Rewards;



            dbContext.SaveChanges();
            return new OptionsProject(dbContextProject);




        }
    }
}
