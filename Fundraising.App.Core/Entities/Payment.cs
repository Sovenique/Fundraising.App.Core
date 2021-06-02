using System;


namespace Fundraising.App.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set;}
        public string CreditCard { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Reward Reward { get; set; }
        public int? RewardId { get; set; }
        public virtual Member Member { get; set; }
        public int? MemberId { get; set; }
        
            
      
    }
}
