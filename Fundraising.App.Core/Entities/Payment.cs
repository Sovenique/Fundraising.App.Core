using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundraising.App.Entities
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
