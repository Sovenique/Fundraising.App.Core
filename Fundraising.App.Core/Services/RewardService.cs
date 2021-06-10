using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                ProjectId = optionReward.ProjectId,
                CreatedDate = optionReward.CreatedDate

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

        // READ / BY PROJECT ID
        // --------------------------------------------------------
        public List<OptionReward> GetAllRewardByProjectId(int ProjectId)
        {
            List<OptionReward> optionRewards = new();
            List<Reward> rewards = _dbContext.Rewards.Where(reward => reward.ProjectId == ProjectId).ToList();
            rewards.ForEach(reward =>
               optionRewards.Add(new OptionReward(reward))
                );
            return optionRewards;
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

        public async Task<Result<OptionReward>> CreateRewardAsync(OptionReward optionReward)
        {
            if (optionReward == null)
            {
                return new Result<OptionReward>(ErrorCode.BadRequest, "Null option.");
            }
            var newReward = new Reward
            {
                Title = optionReward.Title,
                Description = optionReward.Description,
                Value = optionReward.Value,
                ProjectId = optionReward.ProjectId,
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
                return new Result<OptionReward>(ErrorCode.InternalServerError, "Could not save reward.");
            }
            return new Result<OptionReward>
            {
                Data = new OptionReward(newReward)
            };
        }

        public async Task<Result<List<OptionReward>>> GetAllRewardsAsync()
        {
            var rewards = await _dbContext.Rewards.ToListAsync();
            List<OptionReward> optionRewards = new();

            rewards.ForEach(reward =>
                optionRewards.Add(new OptionReward(reward))
            );

            return new Result<List<OptionReward>>
            {
                Data = rewards.Count > 0 ? optionRewards : new List<OptionReward>()
            };
        }

        public async Task<Result<OptionReward>> GetRewardByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return new Result<OptionReward>(ErrorCode.BadRequest, "Invalid ID.");
            }
            var reward = await _dbContext
                .Rewards
                .SingleOrDefaultAsync(cus => cus.Id == Id);
            if (reward == null)
            {
                return new Result<OptionReward>(ErrorCode.NotFound, $"Reward with id : #{Id} not found");
            }
            return new Result<OptionReward>
            {
                Data = new OptionReward(reward)
            };
        }

        public async Task<Result<OptionReward>> UpdateRewardAsync(OptionReward optionReward, int Id)
        {
            var rewardToUpdate = await _dbContext.Rewards.SingleOrDefaultAsync(reward => reward.Id == Id);
            if(rewardToUpdate == null)
            {
                return new Result<OptionReward>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }
            
            rewardToUpdate.Title = optionReward.Title;
            rewardToUpdate.Description = optionReward.Description;
            rewardToUpdate.Value = optionReward.Value;
         

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new Result<OptionReward>(ErrorCode.InternalServerError, "Could not save reward.");
            }
            return new Result<OptionReward>
            {
                Data = new OptionReward(rewardToUpdate)
            };
        }

        public async Task<Result<int>> DeleteRewardByIdAsync(int Id)
        {
            if(Id < 0)
            {
                return new Result<int>(ErrorCode.NotFound, $"Reward with id #{Id} is invalid.");
            }

            var rewardToDelete = await _dbContext.Rewards.SingleOrDefaultAsync(reward => reward.Id == Id);
            if (rewardToDelete == null)
            {
                return new Result<int>(ErrorCode.NotFound, $"Reward with id #{Id} not found.");
            }
            var payments = await _dbContext.Payments.ToListAsync();
            var result_payments = payments.Where(x => x.RewardId == Id);


            foreach (var payment in result_payments)
            {
                _dbContext.Payments.Remove(payment);
            }

            _dbContext.Rewards.Remove(rewardToDelete);
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
    }
}