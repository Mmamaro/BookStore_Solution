using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresAndBooksController : ControllerBase
    {
        private readonly GenreAndBookRepository _genreAndBookRepository;

        public GenresAndBooksController(GenreAndBookRepository genreAndBookRepository)
        {
            _genreAndBookRepository = genreAndBookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreAndBook>>> GetAllGenreAndBooks()
        {
            IEnumerable<GenreAndBook> genresAndBooks = await _genreAndBookRepository.GetAllGenresAndBooksAsync();

            if (genresAndBooks == null)
            {
                return BadRequest();
            }

            return Ok(genresAndBooks);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<GenreAndBook>>> GetGenreAndBookById(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            IEnumerable<GenreAndBook> genreAndBook = await _genreAndBookRepository.GetGenreAndBookByIdAsync(Id);

            if (genreAndBook == null)
            {
                return NotFound();
            }

            return Ok(genreAndBook);

        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchGenreAndBook(string Isbn)
        {

            var genreAndBook = await _genreAndBookRepository.SearchGenreAndBookAsync(Isbn);

            if (genreAndBook == null)
            {
                return NotFound();
            }

            return Ok(genreAndBook);

        }
    }
}
