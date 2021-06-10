using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IRewardService
    {
        public OptionReward CreateReward(OptionReward optionPackage);
        Task<Result<OptionReward>> CreateRewardAsync(OptionReward optionPackage);
        // READ ALL
        public List<OptionReward> GetAllRewards();
        Task<Result<List<OptionReward>>> GetAllRewardsAsync();
        // READ BY ID
        public OptionReward GetRewardById(int Id);
        Task<Result<OptionReward>> GetRewardByIdAsync(int Id);
        // READ BY PROJECT ID
        public List<OptionReward> GetAllRewardByProjectId(int ProjectId);
        // UPDATE
        public OptionReward UpdateReward(OptionReward optionReward, int Id);
        Task<Result<OptionReward>> UpdateRewardAsync(OptionReward optionReward, int Id);
        // DELETE
        public bool DeleteReward(int Id);
        Task<Result<int>> DeleteRewardByIdAsync(int Id);
        Task<Result<List<OptionReward>>> GetMyRewardsAsync(string UserId);


    }
}
