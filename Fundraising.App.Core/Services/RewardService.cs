using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundraising.App.Core.Services
{
    class RewardService : IRewardService
    {
        private readonly FundraisingAppDbContext dbContext;
        

        public RewardService(FundraisingAppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public OptionReward CreateReward(OptionReward optionReward)
        {
            Reward Reward = new()
            {
                Title = optionReward.Title,

                Description = optionReward.Description,

                CreatedDate = DateTime.Now


            };

            dbContext.Rewards.Add(Reward);
            dbContext.SaveChanges();

            return new OptionReward(Reward);


        }

        public bool DeleteReward(int Id)
        {
            Reward dbContextReward = dbContext.Rewards.Find(Id);

            if (dbContextReward == null) return false;

            dbContext.Rewards.Remove(dbContextReward);
            return true;
        }

        public List<OptionReward> GetAllRewards()
        {
            List<Reward> Rewards = dbContext.Rewards.ToList();
            List<OptionReward> optionRewards = new ();
            Rewards.ForEach(Reward => optionRewards.Add(new OptionReward()
            {
                Id = Reward.Id,
                Title = Reward.Title,
                Description = Reward.Description,
                CreatedDate = Reward.CreatedDate,
             }));

            return optionRewards;
        }

        public OptionReward GetRewardById(int Id)
        {
            Reward Reward = dbContext.Rewards.Find(Id);
            if (Reward == null)
            {
                return null;
            }
            return new OptionReward(Reward);
        }

        public OptionReward UpdateReward(OptionReward optionReward, int Id)
        {
            Reward dbContextReward = dbContext.Rewards.Find(Id);
            if (dbContextReward == null) return null;

            dbContextReward.Title = optionReward.Title;
            dbContextReward.Description = optionReward.Description;

            dbContext.SaveChanges();
            return new OptionReward(dbContextReward);
        }
    }
}
