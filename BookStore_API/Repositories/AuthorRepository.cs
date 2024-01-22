using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Data;

namespace BookStore_API.Repositories
{
    public class AuthorRepository
    {
        private readonly DapperContext _Context;

        public AuthorRepository(DapperContext dapperContext)
        {
           _Context = dapperContext;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorAsync()
        {
            const string sql = "select * from authors";

            IDbConnection connection = _Context.CreateConnection();

            var authors = await connection.QueryAsync<Author>(sql);

            return authors;
        }

        public async Task<Author> GetAuthorByIdAsync(int Id)
        {
            var parameter = new { AuthorId = Id };

            const string sql = "select * from authors where AuthorId = @AuthorId";

            using IDbConnection connection = _Context.CreateConnection();

            var author = await connection.QueryFirstOrDefaultAsync<Author>(sql, parameter);

            return author;
        }

        public async Task AddAuthorAsync(Author authorObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("AuthorName", authorObj.AuthorName);
                parameters.Add("AuthorEmail", authorObj.AuthorEmail);

                const string sql = "insert into authors(AuthorName, AuthorEmail) values(@authorName, @AuthorEmail)";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Add author: {ex.Message}");
            }

        }

        public async Task UpdateAuthorAsync(Author authorObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("AuthorId", authorObj.AuthorId);
                parameters.Add("AuthorName", authorObj.AuthorName);
                parameters.Add("AuthorEmail", authorObj.AuthorEmail);

                const string sql = "Update authors set AuthorName = @AuthorName, AuthorEmail = @AuthorEmail where AuthorId = @AuthorId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Update Author: {ex.Message}");
            }

        }

        public async Task DeleteAuthorAsync(int Id)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("AuthorId", Id);

                const string sql = "Delete from authors where AuthorId = @AuthorId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Delete author: {ex.Message}");
            }

        }
    }
}
