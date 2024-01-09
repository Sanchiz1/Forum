using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public bool Liked { get; set; }
    }
}
