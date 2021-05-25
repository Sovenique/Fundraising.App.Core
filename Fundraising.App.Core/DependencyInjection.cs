using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Services;
using Fundraising.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fundraising.App.Core
{
    public static class DependencyInjection
    {
      
            public static IServiceCollection AddCore(this IServiceCollection services)
            {
                services.AddScoped<IMemberService, MemberService>();
                services.AddScoped<IProjectService, ProjectService>();
                services.AddScoped<IRewardService, RewardService>();
                services.AddScoped<IPaymentService, PaymentService>();


            return services;
            }
            public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
            {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
            }
    }




}
