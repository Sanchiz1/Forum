using Application.Common.ViewModels;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<List<PostViewModel>> GetSearchedPostsAsync(GetSearchedPostsQuery getSearchedPostsQuery);
        Task<List<PostViewModel>> GetPostsAsync(GetPostsQuery getPostsQuery);
        Task<List<PostViewModel>> GetUserPostsAsync(GetUserPostsQuery getUserPostsQuery);
        Task<PostViewModel> GetPostByIdAsync(GetPostByIdQuery getPostByIdQuery);
        Task CreatePostAsync(CreatePostCommand createPostCommand);
        Task UpdatePostAsync(UpdatePostCommand updatePostCommand);
        Task AddPostCategoryAsync(AddPostCategoryCommand addPostCategoryCommand);
        Task RemovePostCategoryAsync(RemovePostCategoryCommand removePostCategoryCommand);
        Task DeletePostAsync(DeletePostCommand deletePostCommand);
        Task LikePostAsync(LikePostCommand likePostCommand);
    }
}
