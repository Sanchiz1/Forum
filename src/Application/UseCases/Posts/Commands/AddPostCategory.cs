using Application.Common.Constants;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.UseCases.Posts.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Commands
{
    public record AddPostCategoryCommand : IRequest<Result<string>>
    {
        public int Post_Id { get; set; }
        public int Category_Id { get; set; }
        public int Account_Id { get; set; }
        public string Account_Role { get; set; } = "";
    }
    public class AddPostCategoryCommandHandler : IRequestHandler<AddPostCategoryCommand, Result<string>>
    {
        private readonly IPostRepository _context;

        public AddPostCategoryCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(AddPostCategoryCommand request, CancellationToken cancellationToken)
        {

            if (request.Account_Role != Role.Admin && request.Account_Role != Role.Moderator)
            {
                var post = await _context.GetPostByIdAsync(request.Post_Id);

                if (post.Post.User_Id != request.Account_Id) return new Exception("Permission denied");
            }

            await _context.AddPostCategoryAsync(request);

            return "Category added successfully";
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
