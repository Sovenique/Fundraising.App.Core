using Fundraising.App.Core.Options;
using System.Collections.Generic;

namespace Fundraising.App.Core.Interfaces
{
    public interface IMemberSerivce
    {
        // CREATE
        public OptionMember CreateMember(OptionMember optionMember);
        // READ
        public List<OptionMember> ReadAllMembers();
        public OptionMember GetMemberById(int Id);
        // UPDATE
        public OptionMember UpdateMember(OptionMember optionMember, int Id);
        // DELETE
        public bool DeleteMember(int Id);
    }
}
