using Fundraising.App.Core.Entities;
using System;

namespace Fundraising.App.Core.Options
{
    public class OptionReward
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public decimal Value { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public OptionReward() { }
        public OptionReward(Reward reward)
        {
            Id = reward.Id;
            Title = reward.Title;
            Description = reward.Description;
            Value = reward.Value;
            CreatedDate = DateTime.Now;
            Project = reward.Project;
            ProjectId = reward.ProjectId;
        }

    }

}











