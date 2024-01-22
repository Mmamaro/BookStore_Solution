using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Data;

namespace BookStore_API.Repositories
{
    public class BookReviewUserRepository
    {
        private readonly DapperContext _Context;

        public BookReviewUserRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;
        }

        public async Task<IEnumerable<BookReviewUser>> GetAllBooksReviewsUsersAsync()
        {
            const string sql = @"SELECT b.BookId, b.BookName, b.Isbn, u.UserId, u.UserName,r.ReviewId, r.Rating, r.Comment, r.ReviewDate  
                               FROM books b
                               JOIN reviews r ON r.BookId = b.BookId
                               JOIN users u ON r.UserId = u.UserId;";

            IDbConnection connection = _Context.CreateConnection();

            var booksReviewsUsers = await connection.QueryAsync<BookReviewUser>(sql);

            return booksReviewsUsers;
        }

        public async Task<IEnumerable<BookReviewUser>> GetBooksReviewsUsersByIdAsync(int Id)
        {
            var parameter = new { UserId = Id };

            const string sql = @"SELECT b.BookId, b.BookName, b.Isbn, u.UserId, u.UserName,r.ReviewId, r.Rating, r.Comment, r.ReviewDate  
                               FROM books b
                               JOIN reviews r ON r.BookId = b.BookId
                               JOIN users u ON r.UserId = u.UserId
                               Where u.UserId = @UserId;";

            using IDbConnection connection = _Context.CreateConnection();

            var bookReviewUser = await connection.QueryAsync<BookReviewUser>(sql, parameter);

            return bookReviewUser;
        }

        public async Task<List<BookReviewUser>> SearchBooksReviewsUsersAsync(string Isbn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Isbn", Isbn);

            string sql = @"SELECT b.BookId, b.BookName, b.Isbn, u.UserId, u.UserName,r.ReviewId, r.Rating, r.Comment, r.ReviewDate  
                               FROM books b
                               JOIN reviews r ON r.BookId = b.BookId
                               JOIN users u ON r.UserId = u.UserId
                               Where b.Isbn = @Isbn";


            IDbConnection connection = _Context.CreateConnection();

            var bookReviewUser = await connection.QueryAsync<BookReviewUser>(sql, parameters);

            return bookReviewUser.ToList();

        }


    }
}
