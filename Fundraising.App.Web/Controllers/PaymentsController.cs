using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundraising.App.Core.Entities;
using Fundraising.App.Database;
using Fundraising.App.Web.Services;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Fundraising.App.Web.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRewardService _rewardService;
        private readonly IProjectService _projectService;
        private readonly IPaymentService _paymentService;


        public PaymentsController(ApplicationDbContext context, ICurrentUserService currentUserService, 
            IRewardService rewardService,
            IProjectService projectService,
            IPaymentService paymentService)
        
        
        {
            _context = context;
            _currentUserService = currentUserService;
            _rewardService = rewardService;
            _projectService = projectService;
            _paymentService = paymentService;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.Reward);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Reward)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewBag.RewardId = new SelectList(_context.Rewards, "Id", "Id");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,CreditCard,PaymentDate,RewardId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                var memberId = _currentUserService.UserId;
            
                ViewBag.RewardId = new SelectList(_context.Rewards, "Id", "Id", payment.RewardId).SelectedValue;
                int tmpId = ViewBag.RewardId;
                var reward = _rewardService.GetRewardById(tmpId);
                var project = _projectService.GetProjectById(reward.ProjectId);
                var currentAmount = project.AmountGathered;
                var finalId = project.Id;
                
             
               
               await _paymentService.CreatePaymentAsync(new OptionPayment
                {
                    Id = payment.Id,
                    Amount = payment.Amount,
                    CreditCard = payment.CreditCard,
                    Reward = payment.Reward,
                    PaymentDate = DateTime.Now,
                    RewardId = payment.RewardId,
                    MemberId = memberId
                },  finalId);


          
                return RedirectToAction(nameof(Index));
            }
        
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["RewardId"] = new SelectList(_context.Rewards, "Id", "Id", payment.RewardId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,CreditCard,PaymentDate,RewardId,MemberId")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
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
            ViewData["RewardId"] = new SelectList(_context.Rewards, "Id", "Id", payment.RewardId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Reward)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
