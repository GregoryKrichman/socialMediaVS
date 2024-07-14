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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Relationship>()
               .HasOne(r => r.FollowerUser)
               .WithMany(u => u.Followers)
               .HasForeignKey(r => r.FollowerUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.FollowedUser)
                .WithMany(u => u.Following)
                .HasForeignKey(r => r.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.PostId });
        }
    }
}
