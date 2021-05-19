using Fundraising.App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fundraising.App.Database
{
    public class FundraisingAppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog= Fundraising.App; Integrated Security = true");
        }

        internal Project Find(int id)
        {
            throw new NotImplementedException();
        }

        internal Project Find(object id)
        {
            throw new NotImplementedException();
        }
    }
}
