using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Microsoft.AspNetCore.Authorization;
using Fundraising.App.Web.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.Extensions.Logging;
using Fundraising.App.Core.Services;

namespace Fundraising.App.Web.Controllers
{
    [Authorize]
    public class RewardsController : Controller
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly ILogger<PaymentService> _logger;
        private readonly IRewardService _rewardService;
        private readonly IProjectService _projectService;


        public RewardsController(
            ICurrentUserService currentUserService,
            ILogger<PaymentService> logger,
            IApplicationDbContext context,
            IRewardService rewardService,
            IProjectService projectService
            )
        {
            _currentUserService = currentUserService;
            _context = context;
            _logger = logger;
            _rewardService = rewardService;
            _projectService = projectService;
        }


        // GET: Rewards
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rewards.Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _context.Rewards
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reward == null)
            {
                return NotFound();
            }
            return View(reward);
        }


        public IActionResult Create()
        {
            var Id = _currentUserService.UserId;
            var projects = _projectService.GetAllProjects();
            var currentProjects = projects.Where(x => x.CreatorId == Id).ToList();
            ViewBag.ProjectId = new SelectList(currentProjects, "Id", "Title");
            return View();
        }

        // Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Value,ProjectId")] Reward reward)
        {
            var Id = _currentUserService.UserId;
            var projects = _projectService.GetAllProjects();
            var currentProjects = projects.Where(x => x.CreatorId == Id).ToList();

            ViewBag.ProjectId = new SelectList(currentProjects, "Id", "Title");

            if (ModelState.IsValid)
            {
                _rewardService.CreateReward(new OptionReward
                {
                    Title = reward.Title,
                    Description = reward.Description,
                    Value = reward.Value,
                    ProjectId = reward.ProjectId,
                    CreatedDate = DateTime.Now
                });
                return RedirectToAction(nameof(Index));
            }
            return View(reward);
        }

        // Edit Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _context.Rewards.FindAsync(id);
            if (reward == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", reward.ProjectId);
            return View(reward);
        }

        // Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Value,ProjectId")] Reward reward)
        {
            if (id != reward.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Rewards.Update(reward);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!RewardExists(reward.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", reward.ProjectId);
            return View(reward);
        }

        // Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var payments = await _context.Payments.ToListAsync();
            var result_payments = payments.Where(x => x.RewardId == id);


            foreach (var payment in result_payments)
            {
                 _context.Payments.Remove(payment);
            }
           
            var reward = await _context.Rewards
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reward == null)
            {
                return NotFound();
            }

            return View(reward);
        }

        // Delete Confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payments = await _context.Payments.ToListAsync();
            var result_payments = payments.Where(x => x.RewardId == id);


            foreach (var payment in result_payments)
            {
                _context.Payments.Remove(payment);
            }

            var reward = await _context.Rewards.FindAsync(id);
            _context.Rewards.Remove(reward);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RewardExists(int id)
        {
            return _context.Rewards.Any(e => e.Id == id);
        }
    }




}

