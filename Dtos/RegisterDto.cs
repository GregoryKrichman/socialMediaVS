namespace socialMedia.Dtos
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? CoverPic { get; set; }
        public string? ProfilePic { get; set; }
        public string? City { get; set; }
        public string? Website { get; set; }
    }
}
