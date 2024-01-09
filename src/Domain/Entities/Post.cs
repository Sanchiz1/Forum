using Domain.Common;
using Domain.Constants;
using System;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Edited { get; set; }
        public int User_Id { get; set; }
        public Post() { }

        public bool CanEdit(int Account_Id)
        {
            return this.User_Id == Account_Id;
        }

        public bool CanDelete(int Account_Id, string Account_Role)
        {
            return this.User_Id == Account_Id
                || Account_Role == Roles.Moderator
                || Account_Role == Roles.Admin;
        }

        public bool CanChangeCategories(int Account_Id, string Account_Role)
        {
            return this.User_Id == Account_Id
                || Account_Role == Roles.Moderator
                || Account_Role == Roles.Admin;
        }
    }
}
