using Application.Common.ViewModels;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<List<CommentViewModel>> GetCommentsAsync(GetCommentsQuery getCommentsQuery);
        Task<CommentViewModel> GetCommentByIdAsync(GetCommentByIdQuery getCommentByIdQuery);
        Task CreateCommentAsync(CreateCommentCommand createCommentCommand);
        Task UpdateCommentAsync(UpdateCommentCommand updateCommentCommand);
        Task DeleteCommentAsync(DeleteCommentCommand deleteCommentCommand);
        Task LikeCommentAsync(LikeCommentCommand likeCommentCommand);
    }
}
