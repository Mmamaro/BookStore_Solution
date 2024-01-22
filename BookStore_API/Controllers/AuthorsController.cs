using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorRepository _repo;

        public AuthorsController(AuthorRepository authorRepository)
        {
            _repo = authorRepository;
        }

        //Get All Authors 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            IEnumerable<Author> authors = await _repo.GetAllAuthorAsync();

            if(authors == null)
            {
                return BadRequest();
            }

            return Ok(authors);
        }

        //Get Author By Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int Id)
        {
            if(Id == null)
            {
                return BadRequest();
            }

            var author = await _repo.GetAuthorByIdAsync(Id);

            if(author == null)
            {
                return NotFound();
            }

            return author;

        }

        //Add Author
        [HttpPost]
        public async Task<ActionResult> AddAuthor([FromBody] Author authorObj)
        {
            try
            {
                if (authorObj == null || string.IsNullOrEmpty(authorObj.AuthorName) || authorObj.AuthorName == "string" || authorObj.AuthorEmail == "user@example.com")
                {
                    return BadRequest();
                }

                await _repo.AddAuthorAsync(authorObj);


                return StatusCode(201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Adding Author: {ex.Message}");
                return StatusCode(500);
            }
        }

        //Update Author
        [HttpPut]
        public async Task<ActionResult> UpdateAuthor([FromBody] Author authorObj)
        {
            try
            {
                if (authorObj == null || string.IsNullOrEmpty(authorObj.AuthorName) || authorObj.AuthorName == "string" || authorObj.AuthorEmail == "string")
                {
                    return BadRequest();
                }

                var existingId = await _repo.GetAuthorByIdAsync(authorObj.AuthorId);

                if (existingId == null)
                {
                    return NotFound("Author Does Not Exist");
                }

                await _repo.UpdateAuthorAsync(authorObj);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating Author: {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAuthor(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();

                }

                var existingId = await _repo.GetAuthorByIdAsync(Id);

                if (existingId == null)
                {
                    return NotFound("Author Does Not Exist");
                }

                await _repo.DeleteAuthorAsync(Id);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Deleting Author: {ex.Message}");
                return StatusCode(500);
            }

            
        }


    }
}
