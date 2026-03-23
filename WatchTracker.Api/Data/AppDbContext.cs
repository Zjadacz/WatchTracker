using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Data
{
    internal partial class AppDbContext : IdentityDbContext<IdentityUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public async Task<T> InsertAsync<T>(T newObject)
        {
            Entry(newObject).State = EntityState.Added;
            await SaveChangesAsync();
            return newObject;
        }
    }
}
