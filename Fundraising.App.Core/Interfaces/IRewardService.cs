using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IRewardService
    {
        public OptionReward CreateReward(OptionReward optionPackage);
        // READ ALL
        public List<OptionReward> GetAllRewards();
        // READ BY ID
        public OptionReward GetRewardById(int Id);
        // READ BY PROJECT ID
        public List<OptionReward> GetAllRewardByProjectId(int ProjectId);
        // UPDATE
        public OptionReward UpdateReward(OptionReward optionReward, int Id);
        // DELETE
        public bool DeleteReward(int Id);


    }
}
