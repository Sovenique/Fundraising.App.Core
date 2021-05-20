using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly FundraisingAppDbContext dbContext;

        public PaymentService(FundraisingAppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public OptionPayment CreatePayment(OptionPayment optionPayment)
        {
            Payment payment = new()
            {
                Backer = optionPayment.Backer,
                Reward = optionPayment.Reward,
                PaymentDate = DateTime.Now
            };

            dbContext.Payments.Add(payment);
            dbContext.SaveChanges();

            return new OptionPayment(payment);
        }

        public bool DeletePayment(int Id)
        {
            Payment dbContexPayment = dbContext.Payments.Find(Id);
            if (dbContexPayment == null) return false;
            dbContext.Payments.Remove(dbContexPayment);
            return true;
        }

        public List<OptionPayment> ReadAllPayments()
        {
            List<Payment> payments = dbContext.Payments.ToList();
            List<OptionPayment> optionPayments = new();
            payments.ForEach(payment => optionPayments.Add(new OptionMember(payment)));
            return optionPayments;
        }

        public OptionPayment UpdatePayment(OptionPayment optionPayment, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
