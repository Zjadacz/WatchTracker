using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchTracker.Api.Models.Watched;
using WatchTracker.Api.Services;

namespace WatchTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WatchedController : ControllerBase
    {
        private IWatchedService watchedService;

        public WatchedController(IWatchedService watchedService)
        {
            this.watchedService = watchedService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NewWatchedMovieRequest request)
        {
            // get user id
            var addedMovie = await this.watchedService.AddWatchedMovieAsync(new WatchedMovie
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                DateWatched = request.DateWatched,
                Rating = request.Rating
            });

            return Ok(addedMovie); 
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get(string userId)
        {
            // get user id
            var movies = await this.watchedService.GetWatchedMoviesAsync(userId);
            return Ok(movies);
        }

        public record NewWatchedMovieRequest(string Title, string Description, DateOnly DateWatched, float Rating, string UserId);
    }
}
