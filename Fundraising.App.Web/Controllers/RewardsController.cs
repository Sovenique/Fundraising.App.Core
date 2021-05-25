using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;

namespace Fundraising.App.Web.Controllers
{
    public class RewardsController : Controller
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }


        // GET: Rewards
        public async Task<IActionResult> Index()
        {
            var allRewardsResult = await _rewardService.GetAllRewardsAsync();

            return View(allRewardsResult.Data);
        }

        // GET: Rewards/Details/5
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var reward = await _rewardService.
                GetRewardByIdAsync(Id.Value);

            if (reward.Error != null || reward.Data == null)
            {
                return NotFound();
            }

            return View(reward.Data);
        }

        // GET: Rewards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rewards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProjectId,CreatedDate")] Reward reward)
        {
            if (ModelState.IsValid)
            {
                await _rewardService.
                    CreateRewardAsync(new OptionReward
                    {
                        Title = reward.Title,
                        Description = reward.Description,

                    });

                return RedirectToAction(nameof(Index));
            }
            return View(reward);
        }



        // POST: Rewards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProjectId,CreatedDate")] Reward reward)
        {


            if (id != reward.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {

                await _rewardService.
                   UpdateRewardByIdAsync(new OptionReward
                   {
                       Title = reward.Title,
                       Description = reward.Description,

                   }, id);



                return RedirectToAction(nameof(Index));
            }
            return View(reward);
        }

        // GET: Rewards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _rewardService.GetRewardByIdAsync(id.Value);

            if (reward.Error != null || reward.Data == null)
            {
                return NotFound();
            }

            return View(reward.Data);
        }

        // POST: Rewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rewardService.DeleteRewardByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
