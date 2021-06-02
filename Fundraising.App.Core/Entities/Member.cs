using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace Fundraising.App.Core.Entities
{
    public class Member : IdentityUser

    {
 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; } 
        public DateTime Birthday { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Payment> PayedRewards { get; set;} 
        

    
    }


}
