﻿using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Dapper;
using Domain.Entities;
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

        public UserRepository(DapperContext context)
        {
            _dapperContext = context;
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

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<UserViewModel, User, UserViewModel>(query, (userViewModel, user) =>
            {
                userViewModel.User = user;

                return userViewModel;
            }, getSearchedUsersQuery, splitOn: "Id")).ToList();

            return result;
        }
        public async Task<UserViewModel> GetUserByIdAsync(int User_Id)
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

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<UserViewModel, User, UserViewModel>(query, (userViewModel, user) =>
            {
                userViewModel.User = user;

                return userViewModel;
            }, new { User_Id }, splitOn: "Id")).FirstOrDefault();

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

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<UserViewModel, User, UserViewModel>(query, (userViewModel, user) =>
            {
                userViewModel.User = user;

                return userViewModel;
            }, getUserByEmailQuery, splitOn: "Id")).FirstOrDefault();

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

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<UserViewModel, User, UserViewModel>(query, (userViewModel, user) =>
            {
                userViewModel.User = user;

                return userViewModel;
            }, getUserByEmailQuery, splitOn: "Id")).FirstOrDefault();

            return result;
        }

        public async Task<UserViewModel> GetUserByUsernameOrEmailAsync(string Username_Or_Email)
        {
            UserViewModel result = null;

            string query = $@"SELECT
                COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                WHERE Users.Username = @Username_Or_Email OR Users.Email = @Username_Or_Email";

            using var connection = _dapperContext.CreateConnection();

            result = (await connection.QueryAsync<UserViewModel, User, UserViewModel>(query, (userViewModel, user) =>
            {
                userViewModel.User = user;

                return userViewModel;
            }, new { Username_Or_Email }, splitOn: "Id")).FirstOrDefault();

            return result;
        }
        public async Task<UserViewModel> GetUserByCredentialsAsync(string Username_Or_Email, string Password)
        {
            UserViewModel result = null;

            string query = $@"SELECT
                COALESCE(Roles.Name, 'User') AS Role,
                COALESCE(Users.Role_Id, 0) AS Role_Id,
                Users.Id, Users.Username, Users.Email, Users.Bio, Users.Registered_At
                FROM Users
                LEFT JOIN Roles ON Roles.Id = Users.Role_Id
                WHERE (Users.Username = @Username_Or_Email OR Users.Email = @Username_Or_Email) AND Users.Password = @Password";

            using var connection = _dapperContext.CreateConnection();

            result = (await connection.QueryAsync<UserViewModel, User, UserViewModel>(query, (userViewModel, user) =>
            {
                userViewModel.User = user;

                return userViewModel;
            }, new { Username_Or_Email, Password }, splitOn: "Id")).FirstOrDefault();

            return result;
        }
        public async Task<string> GetUserSaltAsync(int User_id)
        {
            string result;
            string query = $@"SELECT Salt FROM Users WHERE Id = @User_id";

            using var connection = _dapperContext.CreateConnection();

            result = (await connection.QueryAsync<string>(query, new { User_id })).FirstOrDefault();

            return result;
        }
        public async Task<string> GetUserPasswordAsync(int User_id)
        {
            string result;
            string query = $@"SELECT Password FROM Users WHERE Id = @User_id";

            using var connection = _dapperContext.CreateConnection();

            result = (await connection.QueryAsync<string>(query, new { User_id })).FirstOrDefault();

            return result;
        }
        public async Task CreateUserAsync(string Username, string Email, string Bio, string Password, string Salt)
        {
            string query = $@"INSERT INTO Users (Username, Email, Bio, Password, Salt) VALUES (@Username, @Email, @Bio, @Password, @Salt)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { Username, Email, Bio, Password, Salt });
        }
        public async Task UpdateUserAsync(UpdateUserCommand updateUserCommand)
        {
            string query = $@"UPDATE Users SET Username = @Username, Email = @Email, Bio = @Bio WHERE Id = @Account_Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, updateUserCommand);
        }
        public async Task UpdateUserRoleAsync(UpdateUserRoleCommand updateUserRoleCommand)
        {
            string query = $@"UPDATE Users SET Role_Id = @Role_Id WHERE Id = @User_Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, updateUserRoleCommand);
        }

        public async Task ChangeUserPasswordAsync(string Password, string Salt, int User_Id)
        {
            string query = $@"UPDATE Users SET Password = @Password, Salt = @Salt WHERE Id = @User_Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { Salt, Password, User_Id });
        }
        public async Task DeleteUserAsync(DeleteUserCommand deleteUserCommand)
        {
            string query = $@"DELETE FROM Users WHERE Id = @User_Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, deleteUserCommand);
        }
    }
}
