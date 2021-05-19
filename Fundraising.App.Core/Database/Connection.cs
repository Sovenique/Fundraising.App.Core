using Fundraising.App.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Fundraising.App.Database
{
    internal class FundraisingAppDbContext : DbContext
    {
        public DbSet<Member> Member { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Reward> Reward { get; set; }
        public DbSet<Payment> Payment { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog= Fundraising.App; Integrated Security = true");
        }
    }
}
