using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace socialMedia.Models
{
    public class Relationship
    {
        public int Id { get; set; }

        [Required]
        public int FollowerUserId { get; set; }

        [Required]
        public int FollowedUserId { get; set; }

        [JsonIgnore]
        public User? FollowerUser { get; set; }

        [JsonIgnore]
        public User? FollowedUser { get; set; }
    }
}
