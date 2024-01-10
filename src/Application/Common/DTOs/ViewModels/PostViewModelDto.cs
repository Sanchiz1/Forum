using System.Collections.Generic;

namespace Application.Common.DTOs.ViewModels
{
    public class PostViewModelDto
    {
        public PostDto Post { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public string User_Username { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public bool Liked { get; set; }
    }
}
