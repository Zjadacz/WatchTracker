using Microsoft.EntityFrameworkCore;
using WatchTracker.Api.Data;
using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Services
{
    public class WatchedService : IWatchedService
    {
        private IAppDbContext appDbContext;

        public WatchedService(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task<WatchedMovie> AddWatchedMovieAsync(WatchedMovie watchedMovie) => this.appDbContext.AddMovieAsync(watchedMovie);

        public Task<List<WatchedMovie>> GetWatchedMoviesAsync(string userId) => this.appDbContext.GetMovies(userId).ToListAsync();
    }
}
