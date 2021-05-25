using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    public class RewardService : IRewardService
    {
<<<<<<< HEAD
        private readonly IApplicationDbContext dbContext;
        

        public RewardService(IApplicationDbContext _dbContext)
=======
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<RewardService> _logger;

        public RewardService(IApplicationDbContext dbContext, ILogger<RewardService> logger)
>>>>>>> d0db14ba137226f61abded2a42f7b35f09ca7484
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

        public async Task<Result<int>> DeleteRewardByIdAsync(int Id)
        {
            var rewardToDelete = await GetRewardByIdAsync(Id);

            if (rewardToDelete.Error != null || rewardToDelete.Data == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }

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

        public async Task<Result<List<Reward>>> GetAllRewardsAsync()
        {
            var rewards = await _dbContext.Rewards.ToListAsync();

            return new Result<List<Reward>>
            {
                Data = rewards.Count > 0 ? rewards : new List<Reward>()
            };
        }

        public async Task<Result<Reward>> GetRewardByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return new Result<Reward>(ErrorCode.BadRequest, "Id cannot be less than or equal to zero.");
            }

            var reward = await _dbContext
                .Rewards
                .SingleOrDefaultAsync(cus => cus.Id == Id);

            if (reward == null)
            {
                return new Result<Reward>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }

            return new Result<Reward>
            {
                Data = reward
            };
        }

        public async Task<Result<Reward>>UpdateRewardByIdAsync(OptionReward optionReward, int Id)
        {
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