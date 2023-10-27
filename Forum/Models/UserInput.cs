namespace Forum.Models
{
    public class UserInput
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? Password { get; set; }

        public UserInput()
        {
        }

        public UserInput(string username, string email, string bio, string password)
        {
            Username = username;
            Email = email;
            Bio = bio;
            Password = password;
        }
    }
}