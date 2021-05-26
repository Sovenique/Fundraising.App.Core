using Fundraising.App.Core.Options;
using System.Collections.Generic;


namespace Fundraising.App.Core.Interfaces
{
    public interface IProjectService
    {
        public OptionsProject CreateProject(OptionsProject optionProjrct);
        public List<OptionsProject> GetAllProjects();
        public OptionsProject GetOptionsProjectById(int Id);
        public OptionsProject UpdateProject(OptionsProject optionsProject, int Id);
        public bool DeleteProject(int Id);
    }
}
