using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _repo;
        private readonly GenreRepository _genreRepository;
        private readonly AuthorRepository _authorRepository;

        public BooksController(BookRepository repo, GenreRepository genreRepository, AuthorRepository authorRepository)
        {
            _repo = repo;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            IEnumerable<Book> books = await _repo.GetAllBooksAsync();

            if (books == null)
            {
                return BadRequest();
            }

            return Ok(books);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Book>> GetBookById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var book = await _repo.GetBookByIdAsync(Id);

            if (book == null)
            {
                return NotFound();
            }

            return book;

        }

        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] Book bookObj)
        {
            try
            {
                if (bookObj == null || string.IsNullOrEmpty(bookObj.BookName) || bookObj.BookName == "string" 
                    || bookObj.Isbn == "string" || string.IsNullOrEmpty(bookObj.Isbn) || bookObj.Price == 0 
                    || bookObj.AuthorId <= 0 || bookObj.GenreId <= 0)
                {
                    return BadRequest();
                }

                var existingAuthor = await _authorRepository.GetAuthorByIdAsync(bookObj.AuthorId);
                var existingGenre = await _genreRepository.GetGenreByIdAsync(bookObj.GenreId);

                if(existingAuthor == null || existingGenre == null)
                {
                    return BadRequest("The Author or Genre you provided does not exist in the Database");
                }

                await _repo.AddBookAsync(bookObj);


                return StatusCode(201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Adding Book: {ex.Message}");
                return StatusCode(500);
            }
        }

        //Update Author
        [HttpPut]
        public async Task<ActionResult> UpdateAuthor([FromBody] Book bookObj)
        {
            try
            {
                if (bookObj == null || string.IsNullOrEmpty(bookObj.BookName) || bookObj.BookName == "string"
                    || bookObj.Isbn == "string" || string.IsNullOrEmpty(bookObj.Isbn) || bookObj.Price == 0
                    || bookObj.AuthorId <= 0 || bookObj.GenreId <= 0)
                {
                    return BadRequest();
                }

                var existingAuthor = await _authorRepository.GetAuthorByIdAsync(bookObj.AuthorId);
                var existingGenre = await _genreRepository.GetGenreByIdAsync(bookObj.GenreId);

                if (existingAuthor == null || existingGenre == null)
                {
                    return BadRequest("The Author or Genre you provided does not exist in the Database");
                }

                await _repo.UpdateBookAsync(bookObj);


                return StatusCode(201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating The Book: {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBook(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();

                }

                var existingId = await _repo.GetBookByIdAsync(Id);

                if (existingId == null)
                {
                    return NotFound("The Book Does Not Exist");
                }

                await _repo.DeleteBookAsync(Id);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Deleting The Book: {ex.Message}");
                return StatusCode(500);
            }


        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchUser(string? BookName, string? Isbn)
        {

            var user = await _repo.SearchBookAsync(BookName, Isbn);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }
    }
}
