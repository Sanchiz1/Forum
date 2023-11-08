namespace Forum.Models.Comments
{
    public class CommentInput
    {
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
        public CommentInput() { }
    }
}
