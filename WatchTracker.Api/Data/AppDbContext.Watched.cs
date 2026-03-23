using Microsoft.EntityFrameworkCore;
using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Data
{
    internal partial class AppDbContext
    {
        public DbSet<WatchedMovie> WatchedMovies { get; set; }

        public Task<WatchedMovie> AddMovieAsync(WatchedMovie watchedMovie) => this.InsertAsync(watchedMovie);

        public IQueryable<WatchedMovie> GetMovies(Guid userId) => this.WatchedMovies.Where(b => b.UserId == userId);
    }
}
