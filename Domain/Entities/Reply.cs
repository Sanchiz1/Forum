using System;

namespace Domain.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_Id { get; set; }
        public int User_Id { get; set; }
        public string User_Username { get; set; }
        public string Reply_Username { get; set; }
        public int Likes { get; set; }
        public bool Liked { get; set; }
        public Reply() { }
    }
}
