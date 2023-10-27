using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Helpers;
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

            var user = connection.Query<User>(query, new { id }).FirstOrDefault();
            return user;
        }
        public User GetUserByUsername(string username)
        {
            string query = $"SELECT * FROM Users WHERE Username = @username";
            using var connection = _dapperContext.CreateConnection();

            var user = connection.Query<User>(query, new { username }).FirstOrDefault();
            return user;
        }
        public User GetUserByEmail(string email)
        {
            string query = $"SELECT * FROM Users WHERE Email = @email";
            using var connection = _dapperContext.CreateConnection();

            var user = connection.Query<User>(query, new { email }).FirstOrDefault();
            return user;
        }
        public User GetUserByCredentials(string loginOremail, string password)
        {
            using var connection = _dapperContext.CreateConnection();
            var salt = connection.Query<string>($"SELECT Salt FROM Users WHERE Username = @loginOremail OR Email = @loginOremail",
                new { loginOremail, password }).FirstOrDefault();

            if (salt == null) return null;
            string hashedPasssword = PasswordHashHelper.ComputeHash(password, salt);

            var user = connection.Query<User>($"SELECT * FROM Users WHERE (Username = @loginOremail OR Email = @loginOremail) AND Password = @hashedPasssword",
                new { loginOremail, hashedPasssword }).FirstOrDefault();

            return user;
        }
        public void CreateUser(UserInput user)
        {
            try
            {

                string salt = PasswordHashHelper.GenerateSalt();
                string hashedPassword = PasswordHashHelper.ComputeHash(user.Password, salt);
                string query = $"INSERT INTO Users (Username, Email, Bio, Password, Salt) VALUES (@Username, @Email, @Bio, @hashedPassword, @salt)";
                using var connection = _dapperContext.CreateConnection();

                connection.Execute(query, new { user.Username, user.Email, user.Bio, hashedPassword, salt });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
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
