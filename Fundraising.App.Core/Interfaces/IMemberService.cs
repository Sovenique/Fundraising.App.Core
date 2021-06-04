using Fundraising.App.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IMemberService
    {
        public List<OptionMember> GetAllMembers();
    }
}
