using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
