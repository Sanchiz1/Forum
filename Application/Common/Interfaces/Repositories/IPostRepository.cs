using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsAsync(GetPostsQuery getPostsQuery);
        Task<Post> GetPostByIdAsync(GetPostByIdQuery getPostByIdQuery);
        Task CreatePostAsync(CreatePostCommand createPostCommand);
        Task UpdatePostAsync(UpdatePostCommand updatePostCommand);
        Task DeletePostAsync(DeletePostCommand deletePostCommand);
        Task LikePostAsync(LikePostCommand likePostCommand);
    }
}
