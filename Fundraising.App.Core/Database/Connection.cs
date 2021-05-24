using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fundraising.App.Database
{
    public class FundraisingAppDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Payment> Payments { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var LOCAL_DB = true;

            if (!LOCAL_DB)
            {
                optionsBuilder.UseSqlServer("Server=tcp:siraxis.database.windows.net,1433;Initial Catalog=strSqlDemo;Persist Security Info=False;User ID=siraxis;Password=Omadaomada7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
            else
            {
                // Run Migration Commands
                // -------------------------
                // - add-migration fundraisingAppCore
                // - update-database
                optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog= Fundraising.App; Integrated Security = true");
            }
        }

        public override int SaveChanges() 
        {
            return base.SaveChanges();
        }


    }
}
