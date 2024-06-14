using System;
using System.Collections.Generic;

namespace socialMedia.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Desc { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
