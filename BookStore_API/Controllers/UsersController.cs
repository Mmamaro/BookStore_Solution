using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repo;

        public UsersController(UserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            IEnumerable<User> users = await _repo.GetAllUsersAsync();

            if (users == null)
            {
                return BadRequest();
            }

            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUserId(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var user = await _repo.GetUserByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }

            return user;

        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] User userObj)
        {
            try
            {
                if (userObj == null || string.IsNullOrEmpty(userObj.UserName) || userObj.UserName == "string" || userObj.UserEmail == "user@example.com")
                {
                    return BadRequest();
                }

                await _repo.AddUserAsync(userObj);


                return StatusCode(201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Adding User: {ex.Message}");
                return StatusCode(500);
            }
        }


        [HttpPut]
        public async Task<ActionResult> UpdateAuthor([FromBody] User userObj)
        {
            try
            {
                if (userObj == null || string.IsNullOrEmpty(userObj.UserName) || userObj.UserName == "string" || userObj.UserEmail == "user@example.com")
                {
                    return BadRequest();
                }

                var existingId = await _repo.GetUserByIdAsync(userObj.UserId);

                if (existingId == null)
                {
                    return NotFound("User Does Not Exist");
                }

                await _repo.UpdateUserAsync(userObj);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating User: {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();

                }

                var existingId = await _repo.GetUserByIdAsync(Id);

                if (existingId == null)
                {
                    return NotFound("User Does Not Exist");
                }

                await _repo.DeleteUserAsync(Id);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Deleting User: {ex.Message}");
                return StatusCode(500);
            }


        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchUser(string? UserEmail, string? UserName)
        {

            var user = await _repo.SearchUserAsync(UserEmail, UserName);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }

    }
}
