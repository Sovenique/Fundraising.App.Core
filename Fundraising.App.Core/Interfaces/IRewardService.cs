using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    interface IRewardService
    {

        Task<Result<List<Reward>>> GetRewardAsync();
        Task<Result<List<Reward>>> CreateRewardAsync(OptionReward optionReward);
        Task<Result<List<Reward>>> GetRewardByIdAsync(int id);
        Task<Result<List<Reward>>> UpdateRewardAsync(OptionReward optionReward, int id);   
        Task<Result<int>> DeleteRewardByIdAsync(int id);



    }
}
