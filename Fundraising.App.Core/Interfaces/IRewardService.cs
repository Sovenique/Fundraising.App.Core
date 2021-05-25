using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IRewardService
    {
        Task<Result<Reward>> CreateRewardAsync(OptionReward optionReward);

        Task<Result<int>> DeleteRewardByIdAsync(int Id);

        Task<Result<List<Reward>>> GetAllRewardsAsync();

        Task<Result<Reward>> GetRewardByIdAsync(int Id);

        Task<Result<Reward>> UpdateRewardByIdAsync(OptionReward optionReward, int Id);



    }
}
