using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialY.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        public ICollection<UserRating> Ratings { get; set; }

        public Post()
        {
            Ratings = new List<UserRating>();
        }

    }
}
