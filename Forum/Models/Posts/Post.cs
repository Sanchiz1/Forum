namespace Forum.Models.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int User_Id { get; set; }
        public string User_Username { get; set; }
        public Post() { }
    }
}
