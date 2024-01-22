using BookStore_API.Data;
using BookStore_API.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStore_API.Repositories
{
    public class UserRepository
    {
        private readonly DapperContext _Context;

        public UserRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            const string sql = "select * from users";

            IDbConnection connection = _Context.CreateConnection();

            var users = await connection.QueryAsync<User>(sql);

            return users;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            var parameter = new { UserId = Id };

            const string sql = "select * from users where UserId = @UserId";

            using IDbConnection connection = _Context.CreateConnection();

            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, parameter);

            return user;
        }

        public async Task AddUserAsync(User userObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userObj.UserName);
                parameters.Add("UserEmail", userObj.UserEmail);

                const string sql = "insert into users(UserName, UserEmail) values(@UserName, @UserEmail)";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Add User: {ex.Message}");
            }

        }

        public async Task UpdateUserAsync(User userObj)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", userObj.UserId);
                parameters.Add("UserName", userObj.UserName);
                parameters.Add("UserEmail", userObj.UserEmail);

                const string sql = "Update users set UserName = @UserName, UserEmail = @UserEmail where UserId = @UserId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Update User: {ex.Message}");
            }

        }

        public async Task DeleteUserAsync(int Id)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", Id);

                const string sql = "Delete from users where UserId = @UserId";

                using IDbConnection connection = _Context.CreateConnection();

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on The Repo trying to Delete a User: {ex.Message}");
            }

        }

        public async Task<List<User>> SearchUserAsync(string? UserEmail, string? UserName)
        {
                var parameters = new DynamicParameters();
                parameters.Add("UserEmail", UserEmail);
                parameters.Add("UserName", UserName);

                string sql = "select * from users";

                if (UserEmail != null)
                {
                    sql = sql + " where UserEmail = @UserEmail";
                }

                if (UserName != null)
                {
                    if (UserEmail != null)
                    {
                        sql = sql + " and UserName LIKE @UserName";
                    }
                    else
                    {
                        sql = sql + " where UserName LIKE @UserName";
                    }
                }

                IDbConnection connection = _Context.CreateConnection();

                var users = await connection.QueryAsync<User>(sql, parameters);

                return users.ToList();

        }

    }
}
