using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        //CREATE
        public async Task<Result<Reward>> CreateRewardAsync(OptionReward optionReward)
        {
            if (optionReward == null)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Null options.");
            }
            //validation
            if (string.IsNullOrWhiteSpace(optionReward.Title) ||
              string.IsNullOrWhiteSpace(optionReward.Description))
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Not all required reward options provided.");
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
            //operation
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
        //DELETE
        public async Task<Result<int>> DeleteRewardByIdAsync(int Id)
        {
            var rewardToDelete = await GetRewardByIdAsync(Id);
            //validation
            if (rewardToDelete.Error != null || rewardToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }
            //operation
            _dbContext.Rewards.Remove(rewardToDelete.Data);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<int>(ErrorCode.InternalServerError, "Could not delete reward.");
            }

            return new Result<int>
            {
                Data = Id
            };
        }
        //GET ALL
        public async Task<Result<List<Reward>>> GetAllRewardsAsync()
        {
            var rewards = await _dbContext.Rewards.ToListAsync();
            //operation
            return new Result<List<Reward>>
            {
                Data = rewards.Count > 0 ? rewards : new List<Reward>()
            };
        }
        //GET SINGLE
        public async Task<Result<Reward>> GetRewardByIdAsync(int Id)
        {
            //validation
            if (Id <= 0)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }
            //operation
            var reward = await _dbContext
                .Rewards
                .SingleOrDefaultAsync(cus => cus.Id == Id);
            //validation
            if (reward == null)
            {
                return new Result<Reward>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }
            
            return new Result<Reward>
            {
                Data = reward
            };
        }
        //UPDATE
        public async Task<Result<Reward>>UpdateRewardByIdAsync(OptionReward optionReward, int Id)
        {
            //validation
            if (optionReward == null)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Null options.");
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
            var reward = await _dbContext
                .Rewards
                .SingleOrDefaultAsync(cus => cus.Id == Id);

            if (reward == null)
            {
                return new Result<Reward>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }
            //operation
            reward.Title = optionReward.Title;
            reward.Description = optionReward.Description;

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
                Data = reward
            };
        }
    }
}