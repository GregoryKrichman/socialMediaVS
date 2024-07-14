using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace socialMedia.Models
{
    public class Story
    {
        public int Id { get; set; }

        [MaxLength(300)]
        public string? Img { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public User? User { get; set; }
    }
}
