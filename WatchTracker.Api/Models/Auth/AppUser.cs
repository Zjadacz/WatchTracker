using System.ComponentModel.DataAnnotations;

namespace WatchTracker.Api.Models.Auth
{
    /// <summary>
    /// Represents all users properties not included in identity class. All app properties, settings, etc.
    /// </summary>
    public class AppUser
    {
        [Key]
        public string UserId { get; set; }

        /// <summary>
        /// User name should not be used as ID, however it should be unique.
        /// </summary>
        public string UserName { get; set; }
    }
}
