using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IApplicationDbContext dbContext, ILogger<PaymentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<Payment>> CreatePaymentAsync(OptionPayment optionPayment)
        {
            if(optionPayment == null)
            {
                return new Result<Payment>(ErrorCode.BadRequest, "Null option.");
            }
            var newPayment = new Payment
            {
                CreditCard = optionPayment.CreditCard,
                Backer = optionPayment.Backer,
                Reward = optionPayment.Reward,
                PaymentDate = DateTime.Now
            };

            await _dbContext.Payments.AddAsync(newPayment);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Result<Payment>(ErrorCode.InternalServerError, "Could not save reward.");
            }
            return new Result<Payment>
            {
                Data = newPayment
            };
        }

        public async Task<Result<int>> DeletePaymentByIdAsync(int Id)
        {
            var paymentToDelete = await GetPaymentByIdAsync(Id);
            if (paymentToDelete.Error != null || paymentToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Payment with id #{Id} not found.");
            }
            _dbContext.Payments.Remove(paymentToDelete.Data);
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

        public async Task<Result<List<Payment>>> GetAllPaymentsAsync()
        {
            var payments = await _dbContext.Payments.ToListAsync();

            return new Result<List<Payment>>
            {
                Data = payments.Count > 0 ? payments : new List<Payment>()
            };
        }

        public async Task<Result<Payment>> GetPaymentByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return new Result<Payment>(ErrorCode.BadRequest, "Invalid ID.");
            }
            var payment = await _dbContext
                .Payments
                .SingleOrDefaultAsync(cus => cus.Id == Id);
            if (payment == null)
            {
                return new Result<Payment>(ErrorCode.NotFound, $"Project with id : #{Id} not found");
            }
            return new Result<Payment>
            {
                Data = payment
            };
        }


    }
}
