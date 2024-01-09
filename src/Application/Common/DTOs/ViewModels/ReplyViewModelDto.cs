using Application.Common.DTOs;

namespace Application.Common.DTOs.ViewModels
{
    public class ReplyViewModelDto
    {
        public ReplyDto Reply { get; set; }
        public string User_Username { get; set; }
        public string Reply_Username { get; set; }
        public int Likes { get; set; }
        public bool Liked { get; set; }
    }
}
