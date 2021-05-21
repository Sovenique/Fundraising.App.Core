using Fundraising.App.Core.Entities;
using System;


namespace Fundraising.App.Core.Options

{
    public class OptionMember
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

       
        public OptionMember() { }
        public OptionMember(Member Member)
        {
            if (Member != null)
            {
                Id = Member.Id;
                FirstName = Member.FirstName;
                LastName = Member.LastName;
                Address = Member.Address;
                Email = Member.Email;
                Username = Member.Username;
                Password = Member.Password;
                Phone = Member.Phone;
                Birthday = Member.Birthday;
                CreatedDate = Member.CreatedDate;


            }

        }

    }
}
