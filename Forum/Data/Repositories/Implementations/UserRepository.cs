using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Models;
using System;

namespace Forum.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext context)
        {
            _dapperContext = context;
        }

        public List<User> GetUsers()
        {
            string query = $"SELECT * FROM Users";
            using var connection = _dapperContext.CreateConnection();

            var users = connection.Query<User>(query).ToList();
            return users;
        }
        public User GetUserById(int id)
        {
            string query = $"SELECT * FROM Users WHERE Id = @id";
            using var connection = _dapperContext.CreateConnection();

            var user = connection.Query<User>(query, new { id }).First();
            return user;
        }
        public User GetUserByUsername(string username)
        {
            string query = $"SELECT * FROM Users WHERE Username = @username";
            using var connection = _dapperContext.CreateConnection();

            var user = connection.Query<User>(query, new { username }).First();
            return user;
        }
        public User GetUserByEmail(string email)
        {
            string query = $"SELECT * FROM Users WHERE Email = @email";
            using var connection = _dapperContext.CreateConnection();

            var user = connection.Query<User>(query, new { email }).First();
            return user;
        }
        public User GetUserByCredentials(string username, string password)
        {
            string query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password";
            using var connection = _dapperContext.CreateConnection();

            var user = connection.Query<User>(query, new { username, password }).First();
            return user;
        }
        public void CreateUser(User user)
        {
            string query = $"INSERT INTO Users (Username, Email, Bio, Password) VALUES (@Username, @Email, @Bio, @Password)";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { user });
        }
        public void UpdateUser(User user)
        {
            throw new NotImplementedException("Not implemented");
        }
        public void DeleteUser(int id)
        {
            string query = $"DELETE FROM Users WHERE Id = @id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { id });
        }
    }
}
