using Fundraising.App.Core.Options;
using System.Collections.Generic;

namespace Fundraising.App.Core.Interfaces
{
    interface IRewardService
    {

        public OptionReward CreateReward(OptionReward optionReward);
        public List<OptionReward> GetAllRewards();
        public OptionReward GetRewardById(int Id);
        public OptionReward UpdateReward(OptionReward optionReward, int Id);
        public bool DeleteReward(int Id);



    }
}
