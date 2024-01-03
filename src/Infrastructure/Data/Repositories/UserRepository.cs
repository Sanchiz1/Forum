﻿using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using Application.UseCases.Posts.Queries;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Dapper;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public async Task<List<UserViewModel>> GetSearchedUsersAsync(GetSearchedUsersQuery getSearchedUsersQuery)
        {
            List<UserViewModel> result = null;

            string query = $@"SELECT COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                Count(DISTINCT Posts.Id) as Posts,
                 Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                LEFT JOIN Posts ON Posts.User_Id = Users.Id
                LEFT JOIN Comments ON Comments.User_Id = Users.Id
                LEFT JOIN Replies ON Replies.User_Id = Users.Id
                WHERE Users.Registered_At < @User_Timestamp AND Users.Username LIKE '%{getSearchedUsersQuery.Search}%'
                GROUP BY Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At, Roles.Name, Role_Id
                ORDER BY Users.Username DESC OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<UserViewModel, UserDto, UserViewModel>(query, (userViewModel, user) =>
                {
                    userViewModel.User = user;

                    return userViewModel;
                }, getSearchedUsersQuery, splitOn: "Id")).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting searched users");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting searched users");
                throw;
            }

            return result;
        }
        public async Task<UserViewModel> GetUserByIdAsync(GetUserByIdQuery getUserByIdQuery)
        {
            UserViewModel result = null;

            string query = $@"SELECT COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                Count(DISTINCT Posts.Id) as Posts,
                Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                LEFT JOIN Posts ON Posts.User_Id = Users.Id
                LEFT JOIN Comments ON Comments.User_Id = Users.Id
                LEFT JOIN Replies ON Replies.User_Id = Users.Id
                WHERE Users.Id = @User_Id
                GROUP BY Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At, Roles.Name, Role_Id ";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<UserViewModel, UserDto, UserViewModel>(query, (userViewModel, user) =>
                {
                    userViewModel.User = user;

                    return userViewModel;
                }, getUserByIdQuery, splitOn: "Id")).FirstOrDefault();
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

            string query = $@"SELECT COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                Count(DISTINCT Posts.Id) as Posts,
                Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                LEFT JOIN Posts ON Posts.User_Id = Users.Id
                LEFT JOIN Comments ON Comments.User_Id = Users.Id
                LEFT JOIN Replies ON Replies.User_Id = Users.Id
                WHERE Users.Username = @Username
                GROUP BY Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At, Roles.Name, Role_Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<UserViewModel, UserDto, UserViewModel>(query, (userViewModel, user) =>
                {
                    userViewModel.User = user;

                    return userViewModel;
                }, getUserByEmailQuery, splitOn: "Id")).FirstOrDefault();
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

            string query = $@"SELECT COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                Count(DISTINCT Posts.Id) as Posts,
                Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                LEFT JOIN Posts ON Posts.User_Id = Users.Id
                LEFT JOIN Comments ON Comments.User_Id = Users.Id
                LEFT JOIN Replies ON Replies.User_Id = Users.Id
                WHERE Users.Email = @Email
                GROUP BY Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At, Roles.Name, Role_Id ";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<UserViewModel, UserDto, UserViewModel>(query, (userViewModel, user) =>
                {
                    userViewModel.User = user;

                    return userViewModel;
                }, getUserByEmailQuery, splitOn: "Id")).FirstOrDefault();
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

            string query = $@"SELECT
                COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                WHERE (Users.Username = @Username_Or_Email OR Users.Email = @Username_Or_Email) AND Users.Password = @Password";

            try
            {
                using var connection = _dapperContext.CreateConnection();

                var salt = connection.Query<string>($"SELECT Salt FROM Users WHERE Username = @Username_Or_Email OR Email = @Password",
                getUserByCredentialsQuery).FirstOrDefault();

                if (salt == null) return null;

                getUserByCredentialsQuery.Password = PasswordHashHelper.ComputeHash(getUserByCredentialsQuery.Password, salt);

                result = (await connection.QueryAsync<UserViewModel, UserDto, UserViewModel>(query, (userViewModel, user) =>
                {
                    userViewModel.User = user;

                    return userViewModel;
                }, getUserByCredentialsQuery, splitOn: "Id")).FirstOrDefault();
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
        public async Task<bool> CheckUserPasswordAsync(string password, int user_id)
        {
            bool result = false;
            string query = $@"SELECT Password, Salt FROM Users WHERE Id = @user_id";

            try
            {
                using var connection = _dapperContext.CreateConnection();

                HashedPassword hashedPassword = (await connection.QueryAsync<HashedPassword>(query, new { user_id })).FirstOrDefault();

                result = hashedPassword.Password == PasswordHashHelper.ComputeHash(password, hashedPassword.Salt);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting checking user password");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting checking user password");
                throw;
            }

            return result;
        }
        public async Task CreateUserAsync(CreateUserCommand createUserCommand)
        {
            string salt = PasswordHashHelper.GenerateSalt();
            createUserCommand.Password = PasswordHashHelper.ComputeHash(createUserCommand.Password, salt);
            createUserCommand.PasswordSalt = salt;
            string query = $@"INSERT INTO Users (Username, Email, Bio, Password, Salt) VALUES (@Username, @Email, @Bio, @Password, @PasswordSalt)";

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
            string query = $@"UPDATE Users SET Username = @Username, Email = @Email, Bio = @Bio WHERE Id = @User_Id";

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
        public async Task UpdateUserRoleAsync(UpdateUserRoleCommand updateUserRoleCommand)
        {
            string query = $@"UPDATE Users SET Role_Id = @Role_Id WHERE Id = @User_Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, updateUserRoleCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Adding user role");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adding user role");
                throw;
            }
        }
        public async Task ChangeUserPasswordAsync(ChangeUserPasswordCommand changeUserPasswordCommand)
        {
            string query = $@"UPDATE Users SET Password = @Password, Salt = @Salt WHERE Id = @User_Id";

            try
            {
                var Salt = PasswordHashHelper.GenerateSalt();
                var Password = PasswordHashHelper.ComputeHash(changeUserPasswordCommand.New_Password, Salt);
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, new { Salt, Password, changeUserPasswordCommand.User_Id});
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Changing user password");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Changing user password");
                throw;
            }
        }
        public async Task DeleteUserAsync(DeleteUserCommand deleteUserCommand)
        {
            string query = $@"DELETE FROM Users WHERE Id = @User_Id";

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