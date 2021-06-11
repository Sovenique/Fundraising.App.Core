using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Fundraising.App.Web.Areas.Identity.IdentityHostingStartup))]
namespace Fundraising.App.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}