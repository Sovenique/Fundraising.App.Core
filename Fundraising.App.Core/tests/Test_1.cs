<<<<<<< HEAD
﻿using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Options;
using Fundraising.App.Core.Services;
using Fundraising.App.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.tests
{
    public static class Test_1
    {
        /* SCENARIO 1:
         * -----------------------
         * ADD 4 NEW MEMBERS TO MEMBERS DATABASE TABLE
         */
        public static bool Scenario_1()
        {
            Console.WriteLine("*** SCENARIO 1 ***");
            Console.WriteLine("*** ADD 4 NEW MEMBERS ***");

            using FundraisingAppDbContext db = new();
            IMemberService memberService = new MemberService(db);
            

            // add new Member - Items to DataBase
            // ----------------------------------
            List<OptionMember> optionMember = new()
            {
                new OptionMember{
                    FirstName = "Nikos",
                    LastName = "Barolis",
                    Address = null,
                    Email = null,
                    Username = "nbarolis",
                    Password = "1234"
                },
                new OptionMember
                {
                    FirstName = "Christos",
                    LastName = "Ventisle",
                    Address = null,
                    Email = "cventisle@yahoo.gr",
                    Username = "cVentisle",
                    Password = "1234!@#$"
                },
                new OptionMember
                {
                    FirstName = "Ioli",
                    LastName = "Giannakopoulou",
                    Address = "Thivon 12",
                    Email = "ioli.gian@outlook.com",
                    Username = "ioli.gian",
                    Password = "abcdefg"
                },
                new OptionMember
                {
                    FirstName = "Kostas",
                    LastName = "Vasilas",
                    Address = "Alimos",
                    Email = "vasilas.cei@gmail.com",
                    Username = "kvasilas",
                    Password = "12456789"
                }
            };


            // SERVICE: CREATE
            optionMember.ForEach(member => memberService.CreateMember(member));

            Console.WriteLine("*** EXECUTED ***");

            return true;
        }

        /* SCENARIO 2:
         * -----------------------
         * READ ALL MEMBERS FROM MEMBERS DATABASE TABLE
         */
        public static bool Scenario_2()
        {
            Console.WriteLine("*** SCENARIO 2 ***");
            Console.WriteLine("*** READ ALL MEMBERS ***");
            using FundraisingAppDbContext db = new();
            IMemberService memberService = new MemberService(db);

            List<OptionMember> optionMember = new();

            // Read all members and print to console
            optionMember = memberService.ReadAllMembers();
            optionMember.ForEach(member =>
            Console.WriteLine($"{member.FirstName} , {member.LastName} , {member.Username}\n"));

            Console.WriteLine("*** EXECUTED ***");
            return true;
        }

        /* SCENARIO 3:
         * -----------------------
         * DELETE ALL MEMBERS FROM MEMBERS DATABASE TABLE
         */
        public static bool Scenario_3()
        {
            Console.WriteLine("*** SCENARIO 3 ***");
            Console.WriteLine("*** DELETE ALL MEMBERS ***");
            using FundraisingAppDbContext db = new();
            IMemberService memberService = new MemberService(db);

            List<OptionMember> optionMember = new();

            // Read all members and delete them
            optionMember = memberService.ReadAllMembers();
            optionMember.ForEach(member =>
            memberService.DeleteMember(member.Id));

            Console.WriteLine("*** EXECUTED ***");
            return true;
        }
    }
}
=======
﻿//using Fundraising.App.Core.Entities;
//using Fundraising.App.Core.Interfaces;
//using Fundraising.App.Core.Options;
//using Fundraising.App.Core.Services;
//using Fundraising.App.Database;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Fundraising.App.Core.tests
//{
//    public static class Test_1
//    {
//        /* SCENARIO 1:
//         * -----------------------
//         * ADD 4 NEW MEMBERS TO MEMBERS DATABASE TABLE
//         */
//        public static bool Scenario_1()
//        {
//            Console.WriteLine("*** SCENARIO 1 ***");
//            Console.WriteLine("*** ADD 4 NEW MEMBERS ***");

//            using ApplicationDbContext db = new();
//            IMemberService memberService = new MemberService(db);
            

//            // add new Member - Items to DataBase
//            // ----------------------------------
//            List<OptionMember> optionMember = new()
//            {
//                new OptionMember{
//                    FirstName = "Nikos",
//                    LastName = "Barolis",
//                    Address = null,
//                    Email = null,
//                    Username = "nbarolis",
//                    Password = "1234"
//                },
//                new OptionMember
//                {
//                    FirstName = "Christos",
//                    LastName = "Ventisle",
//                    Address = null,
//                    Email = "cventisle@yahoo.gr",
//                    Username = "cVentisle",
//                    Password = "1234!@#$"
//                },
//                new OptionMember
//                {
//                    FirstName = "Ioli",
//                    LastName = "Giannakopoulou",
//                    Address = "Thivon 12",
//                    Email = "ioli.gian@outlook.com",
//                    Username = "ioli.gian",
//                    Password = "abcdefg"
//                },
//                new OptionMember
//                {
//                    FirstName = "Kostas",
//                    LastName = "Vasilas",
//                    Address = "Alimos",
//                    Email = "vasilas.cei@gmail.com",
//                    Username = "kvasilas",
//                    Password = "12456789"
//                }
//            };


//            // SERVICE: CREATE
//            optionMember.ForEach(member => memberService.CreateMember(member));

//            Console.WriteLine("*** EXECUTED ***");

//            return true;
//        }

//        /* SCENARIO 2:
//         * -----------------------
//         * READ ALL MEMBERS FROM MEMBERS DATABASE TABLE
//         */
//        public static bool Scenario_2()
//        {
//            Console.WriteLine("*** SCENARIO 2 ***");
//            Console.WriteLine("*** READ ALL MEMBERS ***");
//            using ApplicationDbContext db = new();
//            IMemberService memberService = new MemberService(db);

//            List<OptionMember> optionMember = new();

//            // Read all members and print to console
//            optionMember = memberService.ReadAllMembers();
//            optionMember.ForEach(member =>
//            Console.WriteLine($"{member.FirstName} , {member.LastName} , {member.Username}\n"));

//            Console.WriteLine("*** EXECUTED ***");
//            return true;
//        }

//        /* SCENARIO 3:
//         * -----------------------
//         * DELETE ALL MEMBERS FROM MEMBERS DATABASE TABLE
//         */
//        public static bool Scenario_3()
//        {
//            Console.WriteLine("*** SCENARIO 3 ***");
//            Console.WriteLine("*** DELETE ALL MEMBERS ***");
//            using FundraisingAppDbContext db = new();
//            IMemberService memberService = new MemberService(db);

//            List<OptionMember> optionMember = new();

//            // Read all members and delete them
//            optionMember = memberService.ReadAllMembers();
//            optionMember.ForEach(member =>
//            memberService.DeleteMember(member.Id));

//            Console.WriteLine("*** EXECUTED ***");
//            return true;
//        }
//    }
//}
>>>>>>> d0db14ba137226f61abded2a42f7b35f09ca7484
