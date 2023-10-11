using Forum.Models;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User GetUserById(int id);
        public User GetUserByUsername(string username);
        public User GetUserByEmail(string email);
        public User GetUserByCredentials(string username, string password);
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int id);
    }
}
