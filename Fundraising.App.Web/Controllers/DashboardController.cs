using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundraising.App.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectService _projectService;
        private readonly IRewardService _rewardService;
        private readonly IPaymentService _paymentService;


        public DashboardController(
            ICurrentUserService currentUserService,
            IProjectService projectService,
            IRewardService rewardService,
            IPaymentService paymentService
            )
        {
            _currentUserService = currentUserService;
            _projectService = projectService;
            _rewardService = rewardService;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
            
        }
        public async Task<IActionResult> Dashboard()
        {
            List<OptionsProject> userOptionProjects = _projectService.GetProjectByCreatorId(_currentUserService.UserId);
            List<OptionsProject> optionProjects = _projectService.GetAllProjects();
            var myProjects = await _projectService.GetMyBackedProjectsAsync(_currentUserService.UserId);
            
            ViewData["User"] = _currentUserService;
            ViewData["UserOptionProjects"] = userOptionProjects;
            ViewData["OptionProjects"] = optionProjects;
            ViewData["myProjects"] = myProjects.Data;

            return View();
        }

        // GET: Main/BuyPackage/5
        public async Task<IActionResult> BuyReward(int ProjectId, string UserId)
        {
            OptionsProject optionProject = _projectService.GetProjectById(ProjectId);
            List<OptionReward> optionRewards = _rewardService.GetAllRewardByProjectId(ProjectId);

            ViewData["optionProject"] = optionProject;
            ViewData["User"] = _currentUserService;
            ViewData["optionRewards"] = optionRewards;

            return View();
        }

        // GET: Main/Payment/5
        public async Task<IActionResult> Payment(int RewardId, int ProjectId)
        {

            OptionReward optionReward = _rewardService.GetRewardById(RewardId);
            OptionsProject optionProject = _projectService.GetProjectById(ProjectId);

            await _paymentService.CreatePaymentAsync(new OptionPayment
            {
                Amount = optionReward.Value,
                CreditCard = "mastercard",
                PaymentDate = DateTime.Now,
                RewardId = optionReward.Id,
                MemberId = _currentUserService.UserId
            }, optionProject.Id);

            return RedirectToAction("Dashboard");
        }

    }
}
