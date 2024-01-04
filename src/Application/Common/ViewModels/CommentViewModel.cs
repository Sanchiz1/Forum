using Application.Common.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModels
{
    public class CommentViewModel
    {
        public CommentDto Comment {  get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Replies { get; set; }
        public bool Liked { get; set; }
    }
}
