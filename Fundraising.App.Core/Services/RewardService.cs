using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    class RewardService : IRewardService
    {
        private readonly FundraisingAppDbContext _dbContext;
        private readonly ILogger<RewardService> _logger;

        public RewardService(FundraisingAppDbContext dbContext, ILogger<RewardService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<Reward>> CreateRewardAsync(OptionReward optionReward)
        {
            if (optionReward == null)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Null options.");
            }

            if (string.IsNullOrWhiteSpace(optionReward.Title) ||
              string.IsNullOrWhiteSpace(optionReward.Description))
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Not all required customer options provided.");
            }

            if (optionReward.Title.Length > 40)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Invalid vat number.");
            }

            var rewardWithSameTitle = await _dbContext.Rewards.SingleOrDefaultAsync(cus => cus.Title == optionReward.Title);

            if (rewardWithSameTitle != null)
            {
                return new Result<Reward>(ErrorCode.Conflict, $"Reward with #{optionReward.Title} already exists.");
            }

            var newReward = new Reward
            {
                Title = optionReward.Title,

                Description = optionReward.Description,

                CreatedDate = DateTime.Now


            };

            await _dbContext.Rewards.AddAsync(newReward);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<Reward>(ErrorCode.InternalServerError, "Could not save reward.");
            }
            return new Result<Reward>
            {
                Data = newReward
            };


        }

        public async Task<Result<Reward>> CreateRewardAsync(int Id)
        {
            Reward dbContextReward = _dbContext.Rewards.Find(Id);

            if (dbContextReward == null) return false;

            _dbContext.Rewards.Remove(dbContextReward);
            return true;
        }

        public List<OptionReward> GetAllRewards()
        {
            List<Reward> Rewards = _dbContext.Rewards.ToList();
            List<OptionReward> optionRewards = new();
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
            Reward Reward = _dbContext.Rewards.Find(Id);
            if (Reward == null)
            {
                return null;
            }
            return new OptionReward(Reward);
        }

        public OptionReward UpdateReward(OptionReward optionReward, int Id)
        {
            Reward dbContextReward = _dbContext.Rewards.Find(Id);
            if (dbContextReward == null) return null;

            dbContextReward.Title = optionReward.Title;
            dbContextReward.Description = optionReward.Description;

            _dbContext.SaveChanges();
            return new OptionReward(dbContextReward);
        }
    }
}