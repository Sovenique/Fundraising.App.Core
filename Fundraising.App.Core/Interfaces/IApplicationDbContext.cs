using Fundraising.App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising.App.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
