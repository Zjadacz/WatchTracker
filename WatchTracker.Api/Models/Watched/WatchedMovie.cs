namespace WatchTracker.Api.Models.Watched
{
    public class WatchedMovie
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DateWatched { get; set; }
        public float Rating { get; set; }
    }
}
