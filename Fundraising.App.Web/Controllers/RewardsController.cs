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
        private readonly ILogger<PaymentService> _logger;
        private readonly IRewardService _rewardService;
        private readonly IProjectService _projectService;
        private readonly IPaymentService _paymentService;


        public RewardsController(
            ICurrentUserService currentUserService,
            ILogger<PaymentService> logger,
            IRewardService rewardService,
            IProjectService projectService,
            IPaymentService paymentService
            )
        {
            _currentUserService = currentUserService;
            _logger = logger;
            _rewardService = rewardService;
            _projectService = projectService;
            _paymentService = paymentService;
        }


        // GET: Rewards
        public async Task<IActionResult> Index()
        {
            var result = await _rewardService.GetAllRewardsAsync();
            return View(result.Data);

        }

        // Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _rewardService.GetRewardByIdAsync(id ?? -1);
            if (reward == null)
            {
                return NotFound();
            }
            return View(reward.Data);
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

            var reward = await _rewardService.GetRewardByIdAsync(id ?? -1);
            if (reward == null)
            {
                return NotFound();
            }
          
            return View(reward.Data);
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
                    OptionReward optionReward = new(reward);
                    await _rewardService.UpdateRewardAsync(optionReward, id);
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (reward != null)
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
           
            return View(reward);
        }

        // Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rewards = await _rewardService.DeleteRewardByIdAsync(id??-1);

            

            return View();
        }

        // Delete Confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
   

            foreach (var payment in payments.Data)
            {
                if (payment.RewardId == id)
                    await _paymentService.DeletePaymentByIdAsync(payment.Id);
            }

           
            await _rewardService.DeleteRewardByIdAsync(id);
        
            return RedirectToAction(nameof(Index));
        }

    }




}

