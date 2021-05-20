using Fundraising.App.Core.Entities;
using System;
using System.Collections.Generic;


namespace Fundraising.App.Core.Options
{
    public class OptionReward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }


        public OptionReward() { }
        public OptionReward(Reward Reward)
        {
            if (Reward != null)
            {
                Id = Reward.Id;
                Title = Reward.Title;
                Description = Reward.Description;
                CreatedDate = Reward.CreatedDate;
            }
        }
    }

}        













