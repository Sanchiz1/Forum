using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Posts.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsAsync(GetCommentsQuery getCommentsQuery);
        Task<Comment> GetCommentByIdAsync(GetCommentByIdQuery getCommentByIdQuery);
        Task CreateCommentAsync(CreateCommentCommand comment);
        Task UpdateCommentAsync(UpdateCommentCommand updateCommentCommand);
        Task DeleteCommentAsync(DeleteCommentCommand deleteCommentCommand);
        Task LikeCommentAsync(LikeCommentCommand likeCommentCommand);
    }
}
