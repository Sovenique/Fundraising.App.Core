using System;
using System.Collections.Generic;


namespace Fundraising.App.Core.Entities
{
    public class Member

    {
 

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        
        public DateTime Birthday { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public List<Payment> PayedRewards { get; set;} 
        

    
    }


}
