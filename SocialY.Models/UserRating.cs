using System.ComponentModel.DataAnnotations;

namespace SocialY.Models
{
    public class UserRating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [Range(0, 10)]
        public int CreativityRating { get; set; }

        [Required]
        [Range(0, 10)]
        public int UniqunessRating { get; set; }

    }
}
