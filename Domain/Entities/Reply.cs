using Domain.Constants;
using System;

namespace Domain.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Edited { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_Id { get; set; }
        public int User_Id { get; set; }
        public Reply() { }
    }
}
