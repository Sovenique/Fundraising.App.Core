using Microsoft.AspNetCore.Http;

namespace Fundraising.App.Web.ViewModel
{
    public class ProjectViewModel
    {
        public string Title { get; set; }
        public string CreatorId { get; set; }
        public IFormFile ProjectImage { get; set; }

    }
}
