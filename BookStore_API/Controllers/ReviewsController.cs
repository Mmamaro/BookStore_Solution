using BookStore_API.Models;
using BookStore_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewRepository _repo;

        public ReviewsController(ReviewRepository reviewRepository)
        {
            _repo = reviewRepository;        
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviewss()
        {
            IEnumerable<Review> reviews = await _repo.GetAllReviewsAsync();

            if (reviews == null)
            {
                return BadRequest();
            }

            return Ok(reviews);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Review>> GetReviewById(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var review = await _repo.GetReviewByIdAsync(Id);

            if (review == null)
            {
                return NotFound();
            }

            return review;

        }

        [HttpPost]
        public async Task<ActionResult> AddReview([FromBody] Review reviewObj)
        {
            try
            {
                if (reviewObj == null || string.IsNullOrEmpty(reviewObj.Comment) || reviewObj.Comment == "string" || reviewObj.Rating < 0 || reviewObj.Rating > 10)
                {
                    return BadRequest();
                }

                await _repo.AddReviewAsync(reviewObj);


                return StatusCode(201);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Adding Review: {ex.Message}");
                return StatusCode(500);
            }
        }

        
        [HttpPut]
        public async Task<ActionResult> UpdateReview([FromBody] Review reviewObj)
        {
            try
            {
                if (reviewObj == null || string.IsNullOrEmpty(reviewObj.Comment) || reviewObj.Comment == "string" || reviewObj.Rating < 0 || reviewObj.Rating > 10)
                {
                    return BadRequest();
                }

                var existingId = await _repo.GetReviewByIdAsync(reviewObj.ReviewId);

                if (existingId == null)
                {
                    return NotFound("Review Does Not Exist");
                }

                await _repo.UpdateReviewAsync(reviewObj);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating Review {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteReview(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();

                }

                var existingId = await _repo.GetReviewByIdAsync(Id);

                if (existingId == null)
                {
                    return NotFound("Review Does Not Exist");
                }

                await _repo.DeleteReviewAsync(Id);

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Deleting Review: {ex.Message}");
                return StatusCode(500);
            }


        }
    }
}
