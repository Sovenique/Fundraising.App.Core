using Fundraising.App.Entities;
using System;

namespace Fundraising.App.Options
{
    public class OptionPayment
    {
        public int Id { get; set; }
        public Member Backer { get; set; }
        public Reward Reward { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
