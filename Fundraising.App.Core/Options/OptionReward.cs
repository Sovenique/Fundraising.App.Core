using Fundraising.App.Core.Entities;
using System.Collections.Generic;


namespace Fundraising.App.Core.Options
{
    public class OptionReward
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Project> Project { get; set; }
    }
}
