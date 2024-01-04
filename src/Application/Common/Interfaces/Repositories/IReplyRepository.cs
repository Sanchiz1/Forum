using Application.Common.ViewModels;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Replies.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IReplyRepository
    {
        Task<List<ReplyViewModel>> GetRepliesAsync(GetRepliesQuery getRepliesQuery);
        Task<ReplyViewModel> GetReplyByIdAsync(GetReplyByIdQuery getReplyByIdQuery);
        Task CreateReplyAsync(CreateReplyCommand createReplyCommand);
        Task UpdateReplyAsync(UpdateReplyCommand updateReplyCommand);
        Task DeleteReplyAsync(DeleteReplyCommand deleteReplyCommand);
        Task LikeReplyAsync(LikeReplyCommand likeReplyCommand);
    }
}
