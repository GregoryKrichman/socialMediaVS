

namespace socialMedia.Dtos
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
        public IFormFile ProfilePic { get; set; }
        public IFormFile CoverPic { get; set; }
    }
}
