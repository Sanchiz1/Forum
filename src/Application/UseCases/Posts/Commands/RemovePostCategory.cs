using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.UseCases.Posts.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Commands
{
    public class RemovePostCategoryCommand : IRequest<Result<string>>
    {
        public int Post_Id { get; set; }
        public int Category_Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class RemovePostCategoryCommandHandler : IRequestHandler<RemovePostCategoryCommand, Result<string>>
    {
        private readonly IPostRepository _context;

        public RemovePostCategoryCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(RemovePostCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role == Role.Admin || request.Account_Role == Role.Moderator)
            {
                await _context.RemovePostCategoryAsync(request);
            }
            else
            {
                var post = await _context.GetPostByIdAsync(new GetPostByIdQuery() { Id = request.Post_Id });

                if (post.Post.User_Id != request.Account_Id)
                {
                    return new Exception("Permission denied");
                }

                await _context.RemovePostCategoryAsync(request);
            }

            return "Category removed successfully";
        }
    }
    public class RemovePostCategoryCommandValidator : AbstractValidator<RemovePostCategoryCommand>
    {
        public RemovePostCategoryCommandValidator()
        {
            RuleFor(c => c.Post_Id)
                .NotEmpty();
            RuleFor(c => c.Category_Id)
                .NotEmpty();
        }
    }
}
