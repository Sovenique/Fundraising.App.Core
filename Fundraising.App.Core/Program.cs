using Fundraising.App.Core.Options;
using Fundraising.App.Core.Interfaces;
using System;
using Fundraising.App.Database;
using Fundraising.App.Core.Services;

namespace Fundraising.App.Core
{
    public class Program
    {
        
        static void Main(string[] args)
        {



            OptionMember test = new()
            {
                FirstName = "Aleka",
                LastName = "Mavriki"



            };

        
            using FundraisingAppDbContext dbContext = new();
            IMemberService memberService = new MemberService(dbContext);
            memberService.CreateMember(test);
         
            
            
        }
    }
}


