namespace Forum.Models.Replies
{
    public class ReplyInput
    {
        public string Text { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_Id { get; set; }
        public int User_Id { get; set; }
        public ReplyInput() { }
    }
}
