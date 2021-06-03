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
        public int? RewardId { get; set; }
        public Member Member { get; set; }
        public string MemberId { get; set; }
        public OptionPayment() { }

        public OptionPayment(Payment Payment)
        {
            Id = Payment.Id;
            Amount = Payment.Amount;
            CreditCard = Payment.CreditCard;
            Reward = Payment.Reward;
            RewardId = Payment.RewardId;
            Member = Payment.Member;
            MemberId = Payment.MemberId;
        }
    }
}





