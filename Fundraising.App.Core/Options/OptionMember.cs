
using Fundraising.App.Entities;
using System;


namespace Fundraising.App

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

        public Member GetMember()
        {
            Member member = new()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Email = Email,
                Username = Username,
                Password = Password,
                Phone = Phone,
                Birthday = Birthday,
                CreatedDate = DateTime.Now
            };
            return member;
        }

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
