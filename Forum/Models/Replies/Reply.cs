namespace Forum.Models.Replies
{
    public class Reply
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Post_Id { get; set; }
        public int Reply_Id { get; set; }
        public int User_Id { get; set; }
        public string User_Username { get; set; }
        public Reply() { }
    }
}
