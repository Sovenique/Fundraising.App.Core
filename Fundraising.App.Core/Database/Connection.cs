using Fundraising.App.Core.Entities;
using Microsoft.EntityFrameworkCore;


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
            optionsBuilder.UseSqlServer("Server=tcp:siraxis.database.windows.net,1433;Initial Catalog=strSqlDemo;Persist Security Info=False;User ID=siraxis;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }


    }
}
