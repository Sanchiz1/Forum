using Domain.Common;
using Domain.Constants;
using System;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Edited { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
        public Comment() { }

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
    }
}