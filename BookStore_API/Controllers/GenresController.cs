using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenreRepository _genreRepo;

        public GenresController(GenreRepository genreRepository)
        {
            _genreRepo = genreRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            IEnumerable<Genre> genres = await _genreRepo.GetAllGenreAsync();

            if(genres == null)
            {
                return BadRequest();
            }

            return Ok(genres);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Genre>> GetGenreById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var genre = await _genreRepo.GetGenreByIdAsync(Id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;

        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor([FromBody] Genre genreObj)
        {
            try
            {
                if (genreObj == null || string.IsNullOrEmpty(genreObj.GenreName) || genreObj.GenreName == "string")
                {
                    return BadRequest();
                }

                await _genreRepo.AddGenreAsync(genreObj);


                return StatusCode(201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Adding Genre: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGenre([FromBody] Genre genreObj)
        {
            try
            {
                if (genreObj == null || string.IsNullOrEmpty(genreObj.GenreName) || genreObj.GenreName == "string")
                {
                    return BadRequest();
                }

                var existingId = await _genreRepo.GetGenreByIdAsync(genreObj.GenreId);

                if (existingId == null)
                {
                    return NotFound("Genre Does Not Exist");
                }

                await _genreRepo.UpdateGenreAsync(genreObj);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating Genre: {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteGenre(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();

                }

                var existingId = await _genreRepo.GetGenreByIdAsync(Id);

                if (existingId == null)
                {
                    return NotFound("Genre Does Not Exist");
                }

                await _genreRepo.DeleteGenreAsync(Id);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Deleting Genre: {ex.Message}");
                return StatusCode(500);
            }


        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchGenre(string GenreName)
        {
            if (GenreName == null || string.IsNullOrEmpty(GenreName) && string.IsNullOrEmpty(GenreName))
            {
                return BadRequest();
            }

            var genre = await _genreRepo.SearchGenreAsync(GenreName);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);

        }


    }
}
