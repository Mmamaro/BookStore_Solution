using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace BookStore_API.Repositories
{
    public class GenreRepository
    {
            private readonly DapperContext _Context;

            public GenreRepository(DapperContext dapperContext)
            {
                _Context = dapperContext;
            }

            public async Task<IEnumerable<Genre>> GetAllGenreAsync()
            {
                const string sql = "select * from genres";

                IDbConnection connection = _Context.CreateConnection();

                var genres = await connection.QueryAsync<Genre>(sql);

                return genres;
            }

            public async Task<Genre> GetGenreByIdAsync(int Id)
            {
                var parameter = new { GenreId = Id };

                const string sql = "select * from genres where GenreId = @GenreId";

                using IDbConnection connection = _Context.CreateConnection();

                var genre = await connection.QueryFirstOrDefaultAsync<Genre>(sql, parameter);

                return genre;
            }

            public async Task AddGenreAsync(Genre genreObj)
            {

                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("GenreName", genreObj.GenreName);
                    parameters.Add("GenreEmail", genreObj.GenreName);

                    const string sql = "insert into genres(GenreName) values(@GenreName)";

                    using IDbConnection connection = _Context.CreateConnection();

                    await connection.ExecuteAsync(sql, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error on The Repo trying to Add Genre: {ex.Message}");
                }

            }

            public async Task UpdateGenreAsync(Genre genreObj)
            {

                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("GenreId", genreObj.GenreId);
                    parameters.Add("GenreName", genreObj.GenreName);


                    const string sql = "Update genres set GenreName = @GenreName where GenreId = @GenreId";

                    using IDbConnection connection = _Context.CreateConnection();

                    await connection.ExecuteAsync(sql, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error on The Repo trying to Update Genre: {ex.Message}");
                }

            }

            public async Task DeleteGenreAsync(int Id)
            {

                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("GenreId", Id);

                    const string sql = "Delete from genres where GenreId = @GenreId";

                    using IDbConnection connection = _Context.CreateConnection();

                    await connection.ExecuteAsync(sql, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error on The Repo trying to delete Genre: {ex.Message}");
                }

            }

            public async Task<List<Genre>> SearchGenreAsync(string? genreName)
            {
                var parameter = new { GenreName = genreName };

                const string sql = "select * from genres where GenreName LIKE @GenreName";

                IDbConnection connection = _Context.CreateConnection();

                var genres = await connection.QueryAsync<Genre>(sql, parameter);

                return genres.ToList();

            }
    }
}
