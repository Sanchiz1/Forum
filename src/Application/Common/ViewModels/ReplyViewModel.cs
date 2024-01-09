using Domain.Entities;

namespace Application.Common.ViewModels
{
    public class ReplyViewModel
    {
        public Reply Reply { get; set; }
        public string User_Username { get; set; }
        public string Reply_Username { get; set; }
        public int Likes { get; set; }
        public bool Liked { get; set; }
    }
}
