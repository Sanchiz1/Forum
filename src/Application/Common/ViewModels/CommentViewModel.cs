using Domain.Entities;

namespace Application.Common.ViewModels
{
    public class CommentViewModel
    {
        public Comment Comment {  get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Replies { get; set; }
        public bool Liked { get; set; }
    }
}
