using Fundraising.App.Core.Entities;

using System;

namespace Fundraising.App.Core.Options
{
    public class OptionPayment
    {
        public int Id { get; set; }
        public Member Backer { get; set; }
        public Reward Reward { get; set; }
        public DateTime PaymentDate { get; set; }


        public OptionPayment() { }
        public OptionPayment(Payment Payment)
        {
            if (Payment != null)
            {
                Id = Payment.Id;
                Backer = Payment.Backer;
                PaymentDate = DateTime.Now;
            }

        }
    }
}





