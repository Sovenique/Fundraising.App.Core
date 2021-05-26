using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Models;
using Fundraising.App.Core.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IMemberService
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



        Task<Result<Member>> CreateMemberAsync(OptionMember optionMember);
        Task<Result<int>> DeleteMemberByIdAsync(int Id);
        Task<Result<List<Member>>> GetAllMembersAsync();
        Task<Result<Member>> GetMemberByIdAsync(int Id);
        Task<Result<Member>> UpdateMemberByIdAsync(OptionMember optionMember, int Id);


    }
}
