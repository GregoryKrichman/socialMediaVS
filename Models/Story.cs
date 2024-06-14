using System;

namespace socialMedia.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Img { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
