using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Data;

namespace BookStore_API.Repositories
{
    public class AuthorAndBookRepository
    {
        private readonly DapperContext _Context;

        public AuthorAndBookRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;
        }

        public async Task<IEnumerable<AuthorAndBook>> GetAllAuthorsAndBooksAsync()
        {
            const string sql = @"SELECT a.AuthorId, a.AuthorName, b.BookName, b.BookId, b.Isbn, b.PublishDate 
                                FROM book_storedb.authors a
                                JOIN book_storedb.books b ON a.AuthorId = b.AuthorId
                                ORDER BY a.AuthorName ASC;";

            IDbConnection connection = _Context.CreateConnection();

            var authorsAndBooks = await connection.QueryAsync<AuthorAndBook>(sql);

            return authorsAndBooks;
        }

        public async Task<IEnumerable<AuthorAndBook>> GetAuthorAndBookByIdAsync(int Id)
        {
            var parameter = new { AuthorId = Id };

            const string sql = @"SELECT a.AuthorId, a.AuthorName, b.BookName, b.BookId, b.Isbn, b.PublishDate 
                                FROM book_storedb.authors a
                                JOIN book_storedb.books b ON a.AuthorId = b.AuthorId
                                WHERE a.AuthorId = @AuthorId;";

            using IDbConnection connection = _Context.CreateConnection();

            var authorAndBook = await connection.QueryAsync<AuthorAndBook>(sql, parameter);

            return authorAndBook;
        }

        public async Task<AuthorAndBook> SearchAuthorAndBookAsync(string Isbn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Isbn", Isbn);

            string sql = @"SELECT a.AuthorId, a.AuthorName, b.BookId, b.BookName, b.Isbn, b.PublishDate 
                                FROM book_storedb.authors a
                                JOIN book_storedb.books b ON a.AuthorId = b.AuthorId
                                WHERE b.Isbn = @Isbn;";


            IDbConnection connection = _Context.CreateConnection();

            var authorAndBook = await connection.QueryFirstOrDefaultAsync<AuthorAndBook>(sql, parameters);

            return authorAndBook;

        }
    }
}
