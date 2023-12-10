using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
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
    public class AddPostCategoryCommand : IRequest
    {
        public int Post_Id { get; set; }
        public int Category_Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class AddPostCategoryCommandHandler : IRequestHandler<AddPostCategoryCommand>
    {
        private readonly IPostRepository _context;

        public AddPostCategoryCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(AddPostCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role == "Admin" || request.Account_Role == "Moderator")
            {
                await _context.AddPostCategoryAsync(request);
            }
            else
            {
                var post = await _context.GetPostByIdAsync(new GetPostByIdQuery() { Id = request.Post_Id });

                if (post.Post.User_Id != request.Account_Id)
                {
                    throw new PermissionException();
                }

                await _context.AddPostCategoryAsync(request);
            }
        }
    }
    public class AddPostCategoryCommandValidator : AbstractValidator<AddPostCategoryCommand>
    {
        public AddPostCategoryCommandValidator()
        {
            RuleFor(c => c.Post_Id)
                .NotEmpty();
            RuleFor(c => c.Category_Id)
                .NotEmpty();
        }
    }
}
