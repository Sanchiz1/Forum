using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Replies.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IReplyRepository
    {
        Task<List<Reply>> GetRepliesAsync(GetRepliesQuery getRepliesQuery);
        Task<Reply> GetReplyByIdAsync(GetReplyByIdQuery getReplyByIdQuery);
        Task CreateReplyAsync(CreateReplyCommand createReplyCommand);
        Task UpdateReplyAsync(UpdateReplyCommand updateReplyCommand);
        Task DeleteReplyAsync(DeleteReplyCommand deleteReplyCommand);
        Task LikeReplyAsync(LikeReplyCommand likeReplyCommand);
    }
}
