using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Dapper;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Linq;
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
        public async Task<UserViewModel> GetUserByIdAsync(GetUserByIdQuery getUserByIdQuery)
        {
            UserViewModel result = null;

            string query = $"SELECT Id, Username, Email, Bio, Registered_At FROM Users WHERE Id = @User_Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<dynamic>(query, getUserByIdQuery)).Select(item =>
                    new UserViewModel()
                    {
                        User = new UserDto()
                        {
                            Id = item.Id,
                            Username = item.Username,
                            Email = item.Email,
                            Bio = item.Bio,
                            Registered_At = item.Registered_At,
                        }
                    }
                ).FirstOrDefault();
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
        public async Task<UserViewModel> GetUserByUsernameAsync(GetUserByUsernameQuery getUserByEmailQuery)
        {
            UserViewModel result = null;

            string query = $"SELECT Id, Username, Email, Bio, Registered_At FROM Users WHERE Username = @Username";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<dynamic>(query, getUserByEmailQuery)).Select(item =>
                    new UserViewModel()
                    {
                        User = new UserDto()
                        {
                            Id = item.Id,
                            Username = item.Username,
                            Email = item.Email,
                            Bio = item.Bio,
                            Registered_At = item.Registered_At,
                        }
                    }
                ).FirstOrDefault();
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
        public async Task<UserViewModel> GetUserByEmailAsync(GetUserByEmailQuery getUserByEmailQuery)
        {
            UserViewModel result = null;

            string query = $"SELECT Id, Username, Email, Bio, Registered_At FROM Users WHERE Email = @Email";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<dynamic>(query, getUserByEmailQuery)).Select(item =>
                    new UserViewModel()
                    {
                        User = new UserDto()
                        {
                            Id = item.Id,
                            Username = item.Username,
                            Email = item.Email,
                            Bio = item.Bio,
                            Registered_At = item.Registered_At,
                        }
                    }
                ).FirstOrDefault();
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
        public async Task<UserViewModel> GetUserByCredentialsAsync(GetUserByCredentialsQuery getUserByCredentialsQuery)
        {
            UserViewModel result = null;

            string query = $"SELECT Id, Username, Email, Bio, Registered_At FROM Users WHERE (Username = @Username_Or_Email OR Email = @Username_Or_Email) AND Password = @Password";

            try
            {
                using var connection = _dapperContext.CreateConnection();

                var salt = connection.Query<string>($"SELECT Salt FROM Users WHERE Username = @Username_Or_Email OR Email = @Password",
                getUserByCredentialsQuery).FirstOrDefault();

                if (salt == null) return null;

                getUserByCredentialsQuery.Password = PasswordHashHelper.ComputeHash(getUserByCredentialsQuery.Password, salt);

                result = (await connection.QueryAsync<dynamic>(query, getUserByCredentialsQuery)).Select(item =>
                    new UserViewModel()
                    {
                        User = new UserDto()
                        {
                            Id = item.Id,
                            Username = item.Username,
                            Email = item.Email,
                            Bio = item.Bio,
                            Registered_At = item.Registered_At,
                        }
                    }
                ).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting user by credentials");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting comment by credentials");
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
            string query = $"UPDATE Users SET Username = @Username, Email = @Email, Bio = @Bio WHERE Id = @User_Id";

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
            string query = $"DELETE FROM Users WHERE Id = @User_Id";

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
