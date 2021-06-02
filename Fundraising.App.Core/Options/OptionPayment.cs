using Fundraising.App.Core.Entities;
using System;

namespace Fundraising.App.Core.Options
{
    public class OptionPayment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string CreditCard { get; set; }
   
        public Reward Reward { get; set; }
        public int? PackageId { get; set; }
        public Member Member { get; set; }
        public int? MemberId { get; set; }
        public OptionPayment() { }

        public OptionPayment(Payment Payment)
        {
            Id = Payment.Id;
            Amount = Payment.Amount;
            CreditCard = Payment.CreditCard;
            Reward = Payment.Reward;
            PackageId = Payment.RewardId;
            Member = Payment.Member;
            MemberId = Payment.MemberId;
        }
    }
}





