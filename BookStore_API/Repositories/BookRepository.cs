using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Data;

namespace BookStore_API.Repositories
{
    public class BookRepository
    {
        private readonly DapperContext _Context;

        public BookRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            const string sql = "select * from books";

            IDbConnection connection = _Context.CreateConnection();

            var books = await connection.QueryAsync<Book>(sql);

            return books;
        }

        public async Task<Book> GetBookByIdAsync(int Id)
        {
            var parameter = new { BookId = Id };

            const string sql = "select * from books where BookId = @BookId";

            using IDbConnection connection = _Context.CreateConnection();

            var book = await connection.QueryFirstOrDefaultAsync<Book>(sql, parameter);

            return book;
        }

        public async Task AddBookAsync(Book bookObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("BookName", bookObj.BookName);
                parameters.Add("Isbn", bookObj.Isbn);
                parameters.Add("PublishDate", bookObj.PublishDate);
                parameters.Add("Price", bookObj.Price);
                parameters.Add("AuthorId", bookObj.AuthorId);
                parameters.Add("GenreId", bookObj.GenreId);

                const string sql = @"insert into books(BookName, Isbn, PublishDate, Price, AuthorId, GenreId) 
                                    values(@BookName, @Isbn, @PublishDate, @Price, @AuthorId, @GenreId)";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in The Repo while trying to Add A Book: {ex.Message}");
            }

        }

        public async Task UpdateBookAsync(Book bookObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("BookId", bookObj.BookId);
                parameters.Add("BookName", bookObj.BookName);
                parameters.Add("Isbn", bookObj.Isbn);
                parameters.Add("PublishDate", bookObj.PublishDate);
                parameters.Add("Price", bookObj.Price);
                parameters.Add("AuthorId", bookObj.AuthorId);
                parameters.Add("GenreId", bookObj.GenreId);

                const string sql = @"Update reviews set BookName = @BookName, Isbn = @Isbn, PublishDate = @PublishDate, Price = @Price,
                                   AuthorId = @AuthorId, GenreId = @GenreId where BookId = @BookId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in The Repo while trying to Update A Book:: {ex.Message}");
            }

        }

        public async Task DeleteBookAsync(int Id)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("BookId", Id);

                const string sql = "Delete from bookss where BookId = @BookId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in The Repo while trying to Delete Book:: {ex.Message}");
            }

        }

        public async Task<List<Book>> SearchBookAsync(string? BookName, string? Isbn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("BookName", BookName);
            parameters.Add("Isbn", Isbn);

            string sql = "select * from books";

            if (Isbn != null)
            {
                sql = sql + " where Isbn = @Isbn";
            }

            if (BookName != null)
            {
                if (Isbn != null)
                {
                    sql = sql + " and BookName LIKE @BookName";
                }
                else
                {
                    sql = sql + " where BookName LIKE @BookName";
                }
            }

            IDbConnection connection = _Context.CreateConnection();

            var books = await connection.QueryAsync<Book>(sql, parameters);

            return books.ToList();

        }

        public async Task UpdateBookTotalPrice(int Id, decimal TotalPrice)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("BookId", Id);
                parameters.Add("TotalPrice", TotalPrice);

                const string sql = @"Update books set TotalPrice = @TotalPrice where BookId = @BookId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in The Repo while trying to Update A Book:: {ex.Message}");
            }

        }

        public async Task UpdateBookDaysPublished(int Id, int daysPublished)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("BookId", Id);
                parameters.Add("DaysPublished", daysPublished);

                const string sql = @"Update books set DaysPublished = @DaysPublished where BookId = @BookId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in The Repo while trying to Update A Book:: {ex.Message}");
            }

        }
    }
}
