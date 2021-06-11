using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace Fundraising.App.Database
{
    public class ApplicationDbContext : IdentityDbContext<Member>, IApplicationDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public override int SaveChanges() 
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }


    }
}
