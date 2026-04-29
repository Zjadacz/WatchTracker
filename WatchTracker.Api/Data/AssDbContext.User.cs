using Microsoft.EntityFrameworkCore;
using WatchTracker.Api.Models.Auth;
using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Data
{
    internal partial class AppDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public async Task<AppUser> AddNewAppUserAsync(string userId)
        {
            int count = await AppUsers.CountAsync();
            var generatedUserName = $"User_{count + 1}";
            var appUser = new AppUser
            {
                UserId = userId,
                UserName = generatedUserName
            };

            return await this.InsertAsync(appUser);
        }
    }
}
