using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Services
{
    public class MemberService : IMemberSerivce
    {
        private readonly FundraisingAppDbContext dbContext;

        public MemberService(FundraisingAppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public OptionMember CreateMember(OptionMember optionMember)
        {
            Member member = new()
            {
                FirstName = optionMember.FirstName,
                LastName = optionMember.LastName,
                Address = optionMember.Address,
                Email = optionMember.Email,
                Username = optionMember.Username,
                Password = optionMember.Password,
                Phone = optionMember.Phone,
                Birthday = optionMember.Birthday,
                CreatedDate = DateTime.Now
            };

            dbContext.Members.Add(member);
            dbContext.SaveChanges();

            return new OptionMember(member);

        }

        public bool DeleteMember(int Id)
        {
            Member dbContexMember = dbContext.Members.Find(Id);
            if (dbContexMember == null) return false;
            dbContext.Members.Remove(dbContexMember);
            return true;
        }

        public List<OptionMember> ReadAllMembers()
        {
            List<Member> members = dbContext.Members.ToList();
            List<OptionMember> optionMembers = new();
            members.ForEach(member => optionMembers.Add(new OptionMember(member)));
            return optionMembers;
        }

        public OptionMember GetMemberById(int Id)
        {
            Member member = dbContext.Members.Find(Id);
            if(member == null)
            {
                return null;
            }
            return new OptionMember(member);
        }

        public OptionMember UpdateMember(OptionMember optionMember, int Id)
        {
            Member dbContextMember = dbContext.Members.Find(Id);
            if (dbContextMember == null) return null;
            // WHAT TO UPDATE?
            // example:
            dbContextMember.FirstName = optionMember.FirstName;

            dbContext.SaveChanges();
            return new OptionMember(dbContextMember);
        }
    }
}
