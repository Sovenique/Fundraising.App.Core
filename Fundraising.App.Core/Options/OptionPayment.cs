using Fundraising.App.Core.Entities;
using System;

namespace Fundraising.App.Core.Options
{
    public class OptionPayment
    {
        public int Id { get; set; }
        public string CreditCard { get; set; }
        public Member Backer { get; set; }
        public Reward Reward { get; set; }
        public DateTime PaymentDate { get; set; }


        public OptionPayment() { }
        public OptionPayment(Payment Payment)
        {
            if (Payment != null)
            {
                Id = Payment.Id;
                CreditCard = Payment.CreditCard;
                Backer = Payment.Backer;
                Reward = Payment.Reward;
                PaymentDate = Payment.PaymentDate;
            }

        }
    }
}





