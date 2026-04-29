namespace WatchTracker.Api.Models.Watched
{
    public class WatchedMovie
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DateWatched { get; set; }
        public float Rating { get; set; }
    }
}
