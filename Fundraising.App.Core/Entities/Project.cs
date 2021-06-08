using System;
using System.Collections.Generic;

namespace Fundraising.App.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal AmountGathered { get; set; }
        public decimal TargetAmount { get; set; }
        public string ImagePath { get; set; }

        public string CreatorId { get; set; }
        public Member Creator { get; set; }
        public List<Reward> Rewards { get; set; }
    }

    public enum Category
    {
        ARTS,
        TECHNOLOGY,
        SCIENCE,
        FOOD,
        MUSIC,
        SOCIAL,
    }

}
