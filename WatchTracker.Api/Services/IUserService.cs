using WatchTracker.Api.Models.Auth;

namespace WatchTracker.Api.Services
{
    public interface IUserService
    {
        Task<AppUser> AddNewAppUserAsync(string userId);
    }
}
