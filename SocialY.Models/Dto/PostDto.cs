using System.ComponentModel.DataAnnotations;

namespace SocialY.Models.Dto
{
    public class PostDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
