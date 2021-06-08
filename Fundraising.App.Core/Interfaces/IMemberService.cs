using Fundraising.App.Core.Options;
using System.Collections.Generic;

namespace Fundraising.App.Core.Interfaces
{
    public interface IMemberService
    {
        public List<OptionMember> GetAllMembers();
    }
}
