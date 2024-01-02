using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.Common.Exceptions;
using Application.UseCases.Posts.Queries;
using Application.Common.Models;
using Application.Common.Constants;
using System;

namespace Application.UseCases.Posts.Commands
{
    public class DeletePostCommand : IRequest<Result<string>>
    {
        public int Id {  get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result<string>>
    {
        private readonly IPostRepository _context;

        public DeletePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken) 
        {
            if (request.Account_Role == Role.Admin || request.Account_Role == Role.Moderator)
            {
                await _context.DeletePostAsync(request);
            }
            else
            {
                var post = await _context.GetPostByIdAsync(new GetPostByIdQuery() { Id = request.Id });

                if (post.Post.User_Id != request.Account_Id)
                {
                    return new Exception("Permission denied");
                }

                await _context.DeletePostAsync(request);
            }

            return "Post deleted successfully";
        }
    }
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}