using System;
using System.Collections.Generic;


namespace Fundraising.App.Core.Entities
{
    public class Reward
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public decimal Value { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
