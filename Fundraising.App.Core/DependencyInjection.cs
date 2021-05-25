using Fundraising.App.Core.Interfaces;
using Fundraising.App.Core.Services;
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
      
    }




}
