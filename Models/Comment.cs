using System;

namespace socialMedia.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Desc { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}
