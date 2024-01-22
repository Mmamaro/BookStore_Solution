using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsAndBooksController : ControllerBase
    {
        private readonly AuthorAndBookRepository _authorAndBookRepository;

        public AuthorsAndBooksController(AuthorAndBookRepository authorAndBookRepository)
        {
            _authorAndBookRepository = authorAndBookRepository;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorAndBook>>> GetAllAuthorsAndBooks()
        {
            IEnumerable<AuthorAndBook> authorsAndBooks = await _authorAndBookRepository.GetAllAuthorsAndBooksAsync();

            if (authorsAndBooks == null)
            {
                return BadRequest();
            }

            return Ok(authorsAndBooks);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<AuthorAndBook>>> GetAuthorAndBookById(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            IEnumerable<AuthorAndBook> authorAndBook = await _authorAndBookRepository.GetAuthorAndBookByIdAsync(Id);

            if (authorAndBook == null)
            {
                return NotFound();
            }

            return Ok(authorAndBook);

        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchAuthorAndBook(string Isbn)
        {

            var authorAndBook = await _authorAndBookRepository.SearchAuthorAndBookAsync(Isbn);

            if (authorAndBook == null)
            {
                return NotFound();
            }

            return Ok(authorAndBook);

        }
    }
}
