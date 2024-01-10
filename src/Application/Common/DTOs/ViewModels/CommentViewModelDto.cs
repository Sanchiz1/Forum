namespace Application.Common.DTOs.ViewModels
{
    public class CommentViewModelDto
    {
        public CommentDto Comment {  get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Replies { get; set; }
        public bool Liked { get; set; }
    }
}
