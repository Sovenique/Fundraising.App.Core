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
        Task<Result<OptionsProject>> CreateProjectAsync(OptionsProject optionsProject);
        // READ ALL
        public List<OptionsProject> GetAllProjects();
        Task<Result<List<OptionsProject>>> GetAllProjectsAsync();
        // READ BY ID
        public OptionsProject GetProjectById(int Id);
        Task<Result<OptionsProject>> GetProjectByIdAsync(int id);
        // READ BY CREATOR ID
        public List<OptionsProject> GetProjectByCreatorId(string CreatorId);
        Task<Result<List<OptionsProject>>> GetProjectByCreatorIdAsync(string CreatorId);
        // UPDATE
        public OptionsProject UpdateProject(OptionsProject optionsProject, int Id);
        Task<Result<OptionsProject>> UpdateProjectAsync(OptionsProject optionsProject, int id);

        // DELETE
        public bool DeleteProject(int Id);
        Task<Result<int>> DeleteProjectAsync(int id);

        Task<Result<List<OptionsProject>>> GetProjectsSearchByTitleAsync(string title_search);
        public OptionsProject UpdateProjectAmount(OptionsProject optionsProject, int Id);

        Task<Result<List<OptionsProject>>> GetMyBackedProjectsAsync(string UserId);
    }
}
