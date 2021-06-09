using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Web.Models;
using Fundraising.App.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Fundraising.App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, ICurrentUserService currentUserService,
            IProjectService projectService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _projectService = projectService;

        }

        public IActionResult Index()
        {
            var trendingProjects = _projectService.GetTrendingProjects(3);
            List<OptionsProject> optionsProjects = new();
            for (int i = 0; i < trendingProjects.Count; i++)
            {
                optionsProjects.Add(trendingProjects.ElementAt(i).Key);
            }
            ViewData["User"] = _currentUserService;
            ViewData["TrendingProjects"] = optionsProjects;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult API()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Home()
        {
            
            
            return View();
        }
    }
}
