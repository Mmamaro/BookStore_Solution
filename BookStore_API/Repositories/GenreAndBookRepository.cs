using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Data;

namespace BookStore_API.Repositories
{
    public class GenreAndBookRepository
    {
        private readonly DapperContext _Context;

        public GenreAndBookRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;
        }

        public async Task<IEnumerable<GenreAndBook>> GetAllGenresAndBooksAsync()
        {
            const string sql = @"SELECT g.GenreId, g.GenreName, b.BookId, b.BookName, b.Isbn  
                                FROM book_storedb.genres g
                                JOIN book_storedb.books b ON g.GenreId = b.GenreId;";

            IDbConnection connection = _Context.CreateConnection();

            var genresAndBooks = await connection.QueryAsync<GenreAndBook>(sql);

            return genresAndBooks;
        }

        public async Task<IEnumerable<GenreAndBook>> GetGenreAndBookByIdAsync(int Id)
        {
            var parameter = new { GenreId = Id };

            const string sql = @"SELECT g.GenreId, g.GenreName, b.BookId, b.BookName, b.Isbn  
                               FROM book_storedb.genres g
                               JOIN book_storedb.books b ON g.GenreId = b.GenreId
                                WHERE g.GenreId = @GenreId;";

            using IDbConnection connection = _Context.CreateConnection();

            var genreAndBook = await connection.QueryAsync<GenreAndBook>(sql, parameter);

            return genreAndBook;
        }

        public async Task<GenreAndBook> SearchGenreAndBookAsync(string Isbn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Isbn", Isbn);

            string sql = @"SELECT g.GenreId, g.GenreName, b.BookId, b.BookName, b.Isbn  
                               FROM book_storedb.genres g
                               JOIN book_storedb.books b ON g.GenreId = b.GenreId
                               WHERE b.Isbn = @Isbn";


            IDbConnection connection = _Context.CreateConnection();

            var genreAndBook = await connection.QueryFirstOrDefaultAsync<GenreAndBook>(sql, parameters);

            return genreAndBook;

        }
    }
}
