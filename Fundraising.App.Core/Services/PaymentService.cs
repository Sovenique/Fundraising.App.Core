using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Fundraising.App.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<PaymentService> _logger;
        private readonly IProjectService _projectService;
        private readonly IRewardService _rewardService;

        public PaymentService(IApplicationDbContext dbContext, ILogger<PaymentService> logger,
            IProjectService projectService, IRewardService rewardService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _projectService = projectService;
            _rewardService = rewardService;
        }

        public async Task<Result<OptionPayment>> CreatePaymentAsync(OptionPayment optionPayment, int Id)
        {
            if (optionPayment == null)
            {
                return new Result<OptionPayment>(ErrorCode.BadRequest, "Null option.");
            }
            var newPayment = new Payment
            {
                CreditCard = optionPayment.CreditCard,
                Member = optionPayment.Member,
                Reward = optionPayment.Reward,
                PaymentDate = DateTime.Now,
                Amount = optionPayment.Amount,
                MemberId = optionPayment.MemberId,
                RewardId = optionPayment.RewardId
            };

            var project = _projectService.GetProjectById(Id);
            var currentAmount = project.AmountGathered;
            var totalAmount = currentAmount + newPayment.Amount;


            _projectService.UpdateProjectAmount(new OptionsProject

            {
                AmountGathered = totalAmount

            }, Id);

            await _dbContext.Payments.AddAsync(newPayment);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Result<OptionPayment>(ErrorCode.InternalServerError, "Could not save reward.");
            }
            return new Result<OptionPayment>
            {
                Data = new OptionPayment(newPayment)
            };
        }

        public async Task<Result<int>> DeletePaymentByIdAsync(int Id)
        {
            var paymentToDelete = await _dbContext.Payments.SingleOrDefaultAsync(payment => payment.Id == Id);
            if (paymentToDelete == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Payment with id #{Id} not found.");
            }
            _dbContext.Payments.Remove(paymentToDelete);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Result<int>(ErrorCode.InternalServerError, $"Could not delete payment with id #{Id}.");
            }
            return new Result<int>
            {
                Data = Id
            };
        }

        public async Task<Result<List<OptionPayment>>> GetAllPaymentsAsync()
        {
            var payments = await _dbContext.Payments.ToListAsync();
            List<OptionPayment> optionPayments = new();

            payments.ForEach(payment =>
                optionPayments.Add(new OptionPayment(payment))
            );

            return new Result<List<OptionPayment>>
            {
                Data = payments.Count > 0 ? optionPayments : new List<OptionPayment>()
            };
        }

        public List<OptionPayment> GetAllPayments()
        {
            var payments = _dbContext.Payments.ToList();
            List<OptionPayment> optionPayments = new();
            payments.ForEach(payment =>
                optionPayments.Add(new OptionPayment(payment))
            );
            return optionPayments;
        }

        public async Task<Result<OptionPayment>> GetPaymentByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return new Result<OptionPayment>(ErrorCode.BadRequest, "Invalid ID.");
            }
            var payment = await _dbContext
                .Payments
                .SingleOrDefaultAsync(cus => cus.Id == Id);
            if (payment == null)
            {
                return new Result<OptionPayment>(ErrorCode.NotFound, $"Project with id : #{Id} not found");
            }
            return new Result<OptionPayment>
            {
                Data = new OptionPayment(payment)
            };
        }


    }
}
