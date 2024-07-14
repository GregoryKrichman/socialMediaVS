using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace socialMedia.Models
{
    public class Post
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string? Desc { get; set; }

        [MaxLength(200)]
        public string? Img { get; set; }

        public int UserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Content { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
