namespace Forum.Models.Posts
{
    public class PostInput
    {
        public string Title { get; set; }
        public string? Text { get; set; }
        public int User_Id { get; set; }
        public PostInput() { }
    }
}
