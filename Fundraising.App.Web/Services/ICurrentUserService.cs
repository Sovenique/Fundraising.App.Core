namespace Fundraising.App.Web.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Name { get; }
        string Email { get; }
    }
}
