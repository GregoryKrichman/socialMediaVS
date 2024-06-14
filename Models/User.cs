using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace socialMedia.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ProfilePic { get; set; } = string.Empty;
        public string CoverPic { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Relationship> Followers { get; set; } = new List<Relationship>();
        public ICollection<Relationship> Followings { get; set; } = new List<Relationship>();
        public ICollection<Story> Stories { get; set; } = new List<Story>();
    }
}
