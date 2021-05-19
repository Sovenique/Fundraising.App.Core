using Fundraising.App.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IProjectService
    {
        public OptionsProject CreateProject(OptionsProject optionProjrct);
        public List<OptionsProject> GetAllProjects();
        public OptionsProject GetOptionsProjectById(int id);
        public OptionsProject UpdateProject(OptionsProject optionsProject, int id);
        public bool DeleteProject(int id);
    }
}
