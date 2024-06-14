namespace socialMedia.Models
{
    public class Relationship
    {
        public int FollowerUserId { get; set; }
        public User Follower { get; set; } = null!;
        public int FollowedUserId { get; set; }
        public User Followed { get; set; } = null!;
    }
}
