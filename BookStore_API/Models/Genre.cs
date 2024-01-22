using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}
