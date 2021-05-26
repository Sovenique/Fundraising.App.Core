using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Result<Project>> CreateProjectAsync(OptionsProject optionProjrct);
        Task<Result<List<Project>>> GetAllProjectsAsync();
       Task<Result<Project>> GetProjectByIdAsync(int Id);
        Task<Result<Project>> UpdateProjectByIdAsync(OptionsProject optionsProject, int Id);
        Task<Result<int>> DeleteProjectByIdAsync(int Id);
    }
}
