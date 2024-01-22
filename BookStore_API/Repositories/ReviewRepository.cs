using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Data;

namespace BookStore_API.Repositories
{
    public class ReviewRepository
    {
        private readonly DapperContext _Context;

        public ReviewRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            const string sql = "select * from reviews";

            IDbConnection connection = _Context.CreateConnection();

            var reviews = await connection.QueryAsync<Review>(sql);

            return reviews;
        }

        public async Task<Review> GetReviewByIdAsync(int Id)
        {
            var parameter = new { ReviewId = Id };

            const string sql = "select * from reviews where ReviewId = @ReviewId";

            using IDbConnection connection = _Context.CreateConnection();

            var review = await connection.QueryFirstOrDefaultAsync<Review>(sql, parameter);

            return review;
        }

        public async Task AddReviewAsync(Review reviewObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Rating", reviewObj.Rating);
                parameters.Add("Comment", reviewObj.Comment);
                parameters.Add("ReviewDate", reviewObj.ReviewDate);
                parameters.Add("BookId", reviewObj.BookId);
                parameters.Add("UserId", reviewObj.UserId);

                const string sql = @"insert into reviews(Rating, Comment, ReviewDate, BookId, UserId) 
                                    values(@Rating, @Comment, @ReviewDate, @BookId, @UserId)";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Add Review: {ex.Message}");
            }

        }

        public async Task UpdateReviewAsync(Review reviewObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ReviewId", reviewObj.ReviewId);
                parameters.Add("Rating", reviewObj.Rating);
                parameters.Add("Comment", reviewObj.Comment);
                parameters.Add("ReviewDate", reviewObj.ReviewDate);
                parameters.Add("BookId", reviewObj.BookId);
                parameters.Add("UserId", reviewObj.UserId);

                const string sql = @"Update reviews set Rating = @Rating, Comment = @Comment, 
                                    ReviewDate = @ReviewDate where ReviewId = @ReviewId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Update Review: {ex.Message}");
            }

        }

        public async Task DeleteReviewAsync(int Id)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ReviewId", Id);

                const string sql = "Delete from reviews where ReviewId = @ReviewId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Delete Review: {ex.Message}");
            }

        }
    }
}
