using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Fundraising.App.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        public string Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    }
}
