namespace BookStore_API.Models
{
    public class AuthorAndBook
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string AuthorName { get; set; }
        public string BookName { get; set; }
        public string Isbn { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
