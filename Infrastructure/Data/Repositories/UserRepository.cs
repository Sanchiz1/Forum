using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Dapper;
using Domain.Entities;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public UserRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }
        public async Task<User> GetUserByIdAsync(GetUserByIdQuery getUserByIdQuery)
        {
            User result = null;

            string query = $"SELECT * FROM Users WHERE Id = @id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<User>(query, getUserByIdQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting user by id");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting user by id");
                throw;
            }

            return result;
        }
        public async Task<User> GetUserByUsernameAsync(GetUserByUsernameQuery getUserByEmailQuery)
        {
            User result = null;

            string query = $"SELECT * FROM Users WHERE Username = @username";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<User>(query, getUserByEmailQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting user by username");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting user by username");
                throw;
            }

            return result;
        }
        public async Task<User> GetUserByEmailAsync(GetUserByEmailQuery getUserByEmailQuery)
        {
            User result = null;

            string query = $"SELECT * FROM Users WHERE Email = @email";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<User>(query, getUserByEmailQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting user by email");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting user by email");
                throw;
            }

            return result;
        }
        public async Task<User> GetUserByCredentialsAsync(GetUserByCredentialsQuery getUserByCredentialsQuery)
        {
            User result = null;

            string query = $"SELECT * FROM Users WHERE (Username = @loginOremail OR Email = @loginOremail) AND Password = @hashedPasssword";

            try
            {
                using var connection = _dapperContext.CreateConnection();

                var salt = connection.Query<string>($"SELECT Salt FROM Users WHERE Username = @loginOremail OR Email = @loginOremail",
                getUserByCredentialsQuery).FirstOrDefault();

                if (salt == null) return null;

                getUserByCredentialsQuery.Password = PasswordHashHelper.ComputeHash(getUserByCredentialsQuery.Password, salt);

                result = (await connection.QueryAsync<User>(query, getUserByCredentialsQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting comment by id");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting comment by id");
                throw;
            }

            return result;
        }
        public async Task CreateUserAsync(CreateUserCommand createUserCommand)
        {
            string salt = PasswordHashHelper.GenerateSalt();
            createUserCommand.Password = PasswordHashHelper.ComputeHash(createUserCommand.Password, salt);

            string query = $"INSERT INTO Users (Username, Email, Bio, Password, Salt) VALUES (@Username, @Email, @Bio, @Password, @salt)";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, createUserCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Creating user");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating user");
                throw;
            }
        }
        public async Task UpdateUserAsync(UpdateUserCommand updateUserCommand)
        {
            string query = $"UPDATE Users SET Username = @Username, Email = @Email, Bio = @Bio WHERE Id = @userId";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, updateUserCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Updating user");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating user");
                throw;
            }
        }
        public async Task DeleteUserAsync(DeleteUserCommand deleteUserCommand)
        {
            string query = $"DELETE FROM Users WHERE Id = @id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, deleteUserCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting user");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting user");
                throw;
            }
        }
    }
}
