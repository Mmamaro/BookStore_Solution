namespace BookStore_API.Models
{
    public class BookReviewUser
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public string BookName { get; set; }
        public string Isbn { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
