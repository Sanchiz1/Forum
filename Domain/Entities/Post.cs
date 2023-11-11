using System;

namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int User_Id { get; set; }
        public string User_Username { get; set; }
        public int Likes {  get; set; }
        public int Comments {  get; set; }
        public bool Liked { get; set; }
        public Post() { }
    }
}
