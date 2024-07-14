using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace socialMedia.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
