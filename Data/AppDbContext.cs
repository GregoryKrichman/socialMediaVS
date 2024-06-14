using Microsoft.EntityFrameworkCore;
using socialMedia.Models;

namespace socialMedia.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Story> Stories { get; set; }
    }
}
