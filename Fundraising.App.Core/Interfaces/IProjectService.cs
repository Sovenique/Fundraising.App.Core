using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IProjectService
    {
        // CREATE
        public OptionsProject CreateProject(OptionsProject optionProject);
        Task<Result<Project>> CreateProjectAsync(OptionsProject optionsProject);
        // READ ALL
        public List<OptionsProject> GetAllProjects();
        Task<Result<List<Project>>> GetAllProjectsAsync();
        // READ BY ID
        public OptionsProject GetProjectById(int Id);
        Task<Result<Project>> GetProjectByIdAsync(int id);
        // READ BY CREATOR ID
        public List<OptionsProject> GetProjectByCreatorId(string CreatorId);
        Task<Result<Project>> GetProjectByCreatorIdAsync(string CreatorId);
        // UPDATE
        public OptionsProject UpdateProject(OptionsProject optionsProject, int Id);
        Task<Result<Project>> UpdateProjectAsync(OptionsProject optionsProject, string CreatorId);

        // DELETE
        public bool DeleteProject(int Id);
        Task<Result<string>> DeleteProjectAsync(string CreatorId);

        public OptionsProject UpdateProjectAmount(OptionsProject optionsProject, int Id);
    }
}
