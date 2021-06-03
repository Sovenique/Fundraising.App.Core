using Fundraising.App.Core.Options;
using System.Collections.Generic;

namespace Fundraising.App.Core.Interfaces
{
    public interface IProjectService
    {
        // CREATE
        public OptionsProject CreateProject(OptionsProject optionProject);
        // READ ALL
        public List<OptionsProject> GetAllProjects();
        // READ BY ID
        public OptionsProject GetProjectById(int Id);
        // UPDATE
        public OptionsProject UpdateProject(OptionsProject optionsProject, int Id);
        // DELETE
        public bool DeleteProject(int Id);

        public OptionsProject UpdateProjectAmount(OptionsProject optionsProject, int Id);
    }
}
