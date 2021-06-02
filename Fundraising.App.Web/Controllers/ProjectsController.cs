using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundraising.App.Core.Entities;
using Fundraising.App.Database;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Microsoft.AspNetCore.Authorization;

namespace Fundraising.App.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var allProjectsResult = await _projectService.GetAllProjectsAsync();
            return View(allProjectsResult.Data);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectService
                .GetProjectByIdAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project.Data);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Category,ProjectStatus,CreatedDate,AmountGathered,TargetAmount")] Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService
                    .CreateProjectAsync(new OptionsProject
                    {
                        Title = project.Title,
                        Description = project.Description,
                        Category = project.Category,
                        CreatedDate = DateTime.Now,
                        TargetAmount = project.TargetAmount,
                        MemberId = ViewBag.UserId
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

            var project = await _projectService
                .GetProjectByIdAsync(id.Value);
            if (project == null || project.Data == null)
            {
                return NotFound();
            }
            return View(project.Data);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Category,ProjectStatus,CreatedDate,AmountGathered,TargetAmount")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{//inside try???
                    await _projectService
                        .UpdateProjectByIdAsync(new OptionsProject
                        {
                            Title = project.Title,
                            Description = project.Description,
                            Category = project.Category,
                            AmountGathered = project.AmountGathered,
                            TargetAmount = project.TargetAmount
                        }, id);

                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ProjectExists(project.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
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

            var project = await _projectService.GetProjectByIdAsync(id.Value);

            if (project.Error != null || project.Data == null)
            {
                return NotFound();
            }

            return View(project.Data);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _projectService.DeleteProjectByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool ProjectExists(int id)
        //{
        //    return _context.Projects.Any(e => e.Id == id);
        //}
    }
}
