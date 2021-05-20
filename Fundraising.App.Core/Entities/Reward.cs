
using System;
using System.Collections.Generic;


namespace Fundraising.App.Core.Entities
{
    public class Reward
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public int ProjectId { get; set; }
        public List<Payment> Payments { set; get; }
        public DateTime CreatedDate { get; set; }
    }
}
