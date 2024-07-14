using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace socialMedia.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string? Desc { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

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
