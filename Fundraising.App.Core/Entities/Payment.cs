using System;


namespace Fundraising.App.Core.Entities
{
    public class Payment
    {
        public decimal Amount { get; set;}
        public int Id { get; set; }
        public Member Backer { get; set; }
        public Reward Reward { get; set; }
        public DateTime PaymentDate { get; set; }
            
      
    }
}
