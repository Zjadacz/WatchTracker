using WatchTracker.Api.Data;
using WatchTracker.Api.Models.Auth;
using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Services
{
    public class UserService : IUserService
    {
        private IAppDbContext appDbContext;

        public UserService(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task<AppUser> AddNewAppUserAsync(string userId) => this.appDbContext.AddNewAppUserAsync(userId);
    }
}
