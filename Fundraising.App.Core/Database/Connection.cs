using Fundraising.App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            optionsBuilder.UseSqlServer("Server=tcp:fundraising.database.windows.net,1433;Initial Catalog=Fundraising.DB;Persist Security Info=False;User ID=fundraising;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }


    }
}
