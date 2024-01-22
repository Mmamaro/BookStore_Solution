using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewUserController : ControllerBase
    {
        private readonly BookReviewUserRepository _bookReviewUserRepository;

        public BookReviewUserController(BookReviewUserRepository bookReviewUserRepository)
        {
            _bookReviewUserRepository = bookReviewUserRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReviewUser>>> GetAllBooksReviewsUsers()
        {
            IEnumerable<BookReviewUser> booksReviewsUsers = await _bookReviewUserRepository.GetAllBooksReviewsUsersAsync();

            if (booksReviewsUsers == null)
            {
                return BadRequest();
            }

            return Ok(booksReviewsUsers);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<BookReviewUser>>> GetBooksReviewsUsersById(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            IEnumerable<BookReviewUser> booksReviewsUsers = await _bookReviewUserRepository.GetBooksReviewsUsersByIdAsync(Id);

            if (booksReviewsUsers == null)
            {
                return NotFound();
            }

            return Ok(booksReviewsUsers);

        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchBooksReviewsUsers(string Isbn)
        {

            var bookReviewUser = await _bookReviewUserRepository.SearchBooksReviewsUsersAsync(Isbn);

            if (bookReviewUser == null)
            {
                return NotFound();
            }

            return Ok(bookReviewUser);

        }
    }
}
