using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundraising.App.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Category Category { get; set; }
        public string ProjectStatus { get; set; }
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
        CHEMICAL,
        ELECTRICAL,
        FOOD,
        MUSIC,
        SOCIAL,
    }

}
