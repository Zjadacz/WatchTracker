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
        public async Task<IActionResult> Add([FromBody] NewWatchedMovie movie)
        {
            // get user id
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var addedMovie = await this.watchedService.AddWatchedMovieAsync(new WatchedMovie
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = movie.Title,
                Description = movie.Description,
                DateWatched = movie.DateWatched,
                Rating = movie.Rating
            });

            return Ok(addedMovie); 
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            // get user id
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var movies = await this.watchedService.GetWatchedMoviesAsync(userId);
            return Ok(movies);
        }

        public record NewWatchedMovie(string Title, string Description, DateOnly DateWatched, float Rating);
    }
}
