using Fundraising.App.Core.ApplicationInsights;
using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Services;
using Fundraising.App.Database;
using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Fundraising.App.Core
{
    public static class DependencyInjection
    {
      
            public static IServiceCollection AddCore(this IServiceCollection services)
            {
                services.AddScoped<IProjectService, ProjectService>();
                services.AddScoped<IRewardService, RewardService>();
                services.AddScoped<IPaymentService, PaymentService>();
                services.AddScoped<IMemberService, MemberService>();
                services.AddIdentity<Member, IdentityRole>()
                       .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Member, IdentityRole>>()
                       .AddEntityFrameworkStores<ApplicationDbContext>()
                       .AddDefaultTokenProviders()
                       .AddDefaultUI();

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

        public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {

            // The following line enables Application Insights telemetry collection.
            var applicationInsightsSettings = new ApplicationInsightsSettings();
            configuration.Bind(ApplicationInsightsSettings.SectionKey, applicationInsightsSettings);
            services.AddSingleton(applicationInsightsSettings);

     
            services.AddSingleton<ITelemetryInitializer, CloudRoleTelemetryInitializer>();

            // Remove a specific built-in telemetry initializer
            var telemetryInitializerToRemove = services.FirstOrDefault<ServiceDescriptor>
                                (t => t.ImplementationType == typeof(AspNetCoreEnvironmentTelemetryInitializer));

            if (telemetryInitializerToRemove != null)
            {
                services.Remove(telemetryInitializerToRemove);
            }

            // You can add custom telemetry processors to TelemetryConfiguration by using the extension method AddApplicationInsightsTelemetryProcessor on IServiceCollection. 
            // You use telemetry processors in advanced filtering scenarios
            services.AddApplicationInsightsTelemetryProcessor<StaticWebAssetsTelemetryProcessor>();


            services.AddApplicationInsightsTelemetry();

            return services;
        }
    }




}
