using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        [EmailAddress]
        public string AuthorEmail { get; set; }             
    }
}
