using Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModels
{
    public class PostViewModel
    {
        public PostDto Post { get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public bool Liked { get; set; }
    }
}
