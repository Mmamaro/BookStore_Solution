using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public int DaysPublished { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal TotalPrice{ get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int GenreId { get; set; }
    }
}
