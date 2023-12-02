using Domain.Constants;
using System;

namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Edited { get; set; }
        public int User_Id { get; set; }
        public Post() { }
    }
}
