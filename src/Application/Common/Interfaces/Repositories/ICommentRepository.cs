﻿using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
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
        Task<List<CommentViewModel>> GetCommentsAsync(GetCommentsQuery getCommentsQuery);
        Task<CommentViewModel> GetCommentByIdAsync(GetCommentByIdQuery getCommentByIdQuery);
        Task CreateCommentAsync(CreateCommentCommand createCommentCommand);
        Task UpdateCommentAsync(UpdateCommentCommand updateCommentCommand);
        Task DeleteCommentAsync(DeleteCommentCommand deleteCommentCommand);
        Task LikeCommentAsync(LikeCommentCommand likeCommentCommand);
    }
}
