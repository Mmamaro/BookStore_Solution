using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
