using WatchTracker.Api.Models.Auth;
using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Data
{
    public interface IAppDbContext
    {
        Task<WatchedMovie> AddMovieAsync(WatchedMovie watchedMovie);
        IQueryable<WatchedMovie> GetMovies(string userId);

        Task<AppUser> AddNewAppUserAsync(string userId);
    }
}
