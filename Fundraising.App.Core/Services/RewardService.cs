using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Fundraising.App.Core.Services
{
    public class RewardService : IRewardService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<RewardService> _logger;

        public RewardService(IApplicationDbContext dbContext, ILogger<RewardService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        // CREATE
        // --------------------------------------------------------
        public OptionReward CreateReward(OptionReward optionReward)
        {


            Reward reward = new()
            {
                Title = optionReward.Title,
                Description = optionReward.Description,
                Value = optionReward.Value,
                ProjectId = optionReward.ProjectId

            };

            _dbContext.Rewards.Add(reward);
            _dbContext.SaveChanges();

            return new OptionReward(reward);
        }

        // DELETE
        // --------------------------------------------------------
        public bool DeleteReward(int Id)
        {
            Reward dbContextReward = _dbContext.Rewards.Find(Id);

            if (dbContextReward == null) return false;

            _dbContext.Rewards.Remove(dbContextReward);
            _dbContext.SaveChanges();
            return true;
        }

        // READ / ALL
        // --------------------------------------------------------
        public List<OptionReward> GetAllRewards()
        {
            List<Reward> rewards = _dbContext.Rewards.ToList();
            List<OptionReward> optionReward = new();
            rewards.ForEach(reward => optionReward.Add(new OptionReward(reward)));
            return optionReward;
        }

        // READ / BY ID
        // --------------------------------------------------------
        public OptionReward GetRewardById(int Id)
        {
            Reward reward = _dbContext.Rewards.Find(Id);
            if (reward == null)
            {
                return null;
            }
            return new OptionReward(reward);
        }

        // UPDATE
        // --------------------------------------------------------
        public OptionReward UpdateReward(OptionReward optionReward, int Id)
        {
            Reward dbContextReward = _dbContext.Rewards.Find(Id);
            if (dbContextReward == null) return null;

            dbContextReward.Title = optionReward.Title;
            dbContextReward.Description = optionReward.Description;
            dbContextReward.Value = optionReward.Value;
            dbContextReward.Project = optionReward.Project;

            _dbContext.SaveChanges();
            return new OptionReward(dbContextReward);

        }
    }
}