using WatchTracker.Api.Models.Watched;

namespace WatchTracker.Api.Services
{
    public interface IWatchedService
    {
        Task<WatchedMovie> AddWatchedMovieAsync(WatchedMovie watchedMovie);
        Task<List<WatchedMovie>> GetWatchedMoviesAsync(string userId);
    }
}