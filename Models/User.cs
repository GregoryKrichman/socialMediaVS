using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace socialMedia.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? CoverPic { get; set; }

        [MaxLength(500)]
        public string? ProfilePic { get; set; }

        [MaxLength(45)]
        public string? City { get; set; }

        [MaxLength(255)]
        public string? Website { get; set; }

        [JsonIgnore]
        public ICollection<Post?> Posts { get; set; }

        [JsonIgnore]
        public ICollection<Story?> Stories { get; set; }

        [JsonIgnore]
        public ICollection<Relationship?> Following { get; set; }

        [JsonIgnore]
        public ICollection<Relationship?> Followers { get; set; }
    }
}
