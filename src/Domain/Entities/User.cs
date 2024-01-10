using Domain.Common;
using Domain.Constants;
using System;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
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

        public bool CanEdit(int Account_Id)
        {
            return this.Id == Account_Id;
        }
        public bool CanDelete(int Account_Id, string Account_Role)
        {
            return this.Id == Account_Id
                || Account_Role == Roles.Moderator
                || Account_Role == Roles.Admin;
        }
    }
}
