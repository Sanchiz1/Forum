using Application.Common.DTOs.ViewModels;
using Application.Common.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostViewModel, PostViewModelDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentViewModel, CommentViewModelDto>();
            CreateMap<Reply, ReplyDto>();
            CreateMap<ReplyViewModel, ReplyViewModelDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserViewModel, UserViewModelDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
