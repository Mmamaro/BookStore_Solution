namespace BookStore_API.Models
{
    public class GenreAndBook
    {
        public int GenreId { get; set; }
        public int BookId { get; set; }
        public string GenreName { get; set; }
        public string BookName { get; set; }
        public string Isbn { get; set; }
    }
}
