using Domain.Constants;
using System;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime Registered_At { get; set; }

        public User() { }

        public User(string username, string email, string bio)
        {
            Id = 0;
            Username = username;
            Email = email;
            Bio = bio;
            Registered_At = DateTime.Now;
        }
    }
}
