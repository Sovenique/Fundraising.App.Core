using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Microsoft.AspNetCore.Authorization;
using Fundraising.App.Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Fundraising.App.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly IProjectService _projectService;
        private readonly IRewardService _rewardService;

        public ProjectsController(ICurrentUserService currentUserService,
            IApplicationDbContext context,
            IProjectService projectService, IRewardService rewardService)

        {
            _currentUserService = currentUserService;
            _context = context;
            _projectService = projectService;
            _rewardService = rewardService;

        }


        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var result = await _projectService.GetAllProjectsAsync();
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Index(string projectSearch)
        {
            ViewData["ProjectDetails"] = projectSearch;
            var result =  await _projectService.GetProjectsSearchByTitleAsync(projectSearch);
            return View(result.Data);

        }









        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _projectService.GetProjectByIdAsync(id ?? -1);
            return View(project.Data);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = _currentUserService.UserId;

            // PROJECT CATEGORY SELECT LIST
            var enumCategory = from Category e in Enum.GetValues(typeof(Fundraising.App.Core.Entities.Category))
                               select new
                               {
                                   ID = (int)e,
                                   Name = e.ToString()
                               };
            ViewData["ProjectCategories"] = new SelectList(enumCategory, "ID", "Name");


            return View();
        }

        // POST: Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedDate,Category,ProjectStatus,AmountGathered,TargetAmount,CreatorId")] Project project, [Bind("file")] IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ProjectImages", file.FileName);
                using (var stream = new FileStream(filePath,
                FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _projectService.CreateProject(new OptionsProject
                {
                    Title = project.Title,
                    Description = project.Description,
                    Category = project.Category,
                    CreatedDate = DateTime.Now,
                    TargetAmount = project.TargetAmount,
                    CreatorId = _currentUserService.UserId,
                    ImagePath = file.FileName
                });

                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }


        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetProjectByIdAsync(id ?? -1);
            if (project == null)
            {
                return NotFound();
            }
            
            
       
            // PROJECT CATEGORY SELECT LIST
            var enumCategory = from Category e in Enum.GetValues(typeof(Category))
                    select new
                    {
                        ID = (int)e,
                        Name = e.ToString()
                    };
            ViewData["ProjectCategories"] = new SelectList(enumCategory, "ID", "Name");


            return View(project.Data);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Category,Status,TargetAmount,CreatorId")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
   
                try
                {
                    OptionsProject optionsProject = new(project);
                    await _projectService.UpdateProjectAsync(optionsProject, project.Id);
                }
                catch 
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _projectService.GetProjectByIdAsync(id ?? -1);
            return View(project.Data);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rewards = await _rewardService.GetAllRewardsAsync();

            foreach (var reward in rewards.Data)
            {
                if (reward.ProjectId == id)
                    await _rewardService.DeleteRewardByIdAsync(reward.Id);
            }
            var project = await _projectService.GetProjectByIdAsync(id);
            await _projectService.DeleteProjectAsync(project.Data.Id);
            
            return RedirectToAction(nameof(Index));
        }

    }
}