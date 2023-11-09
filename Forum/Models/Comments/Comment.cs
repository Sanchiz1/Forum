namespace Forum.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Replies { get; set; }
        public bool Liked { get; set; }
        public Comment() { }
    }
}