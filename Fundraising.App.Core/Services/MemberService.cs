using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly IApplicationDbContext _dbContext;

        public MemberService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<OptionMember> GetAllMembers()
        {
            List<Member> members = _dbContext.Members.ToList();
            List<OptionMember> optionMembers = new();
            members.ForEach(member =>
                optionMembers.Add(new OptionMember(member))
            );
            return optionMembers;

        }
    }
}
