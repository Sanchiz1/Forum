using Application.Common.Constants;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Commands
{
    public class DeleteReplyCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand, Result<string>>
    {
        private readonly IReplyRepository _context;

        public DeleteReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeleteReplyCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role == Role.Admin || request.Account_Role == Role.Moderator)
            {
                await _context.DeleteReplyAsync(request);
            }
            else
            {
                var post = await _context.GetReplyByIdAsync(new Queries.GetReplyByIdQuery() { Id = request.Id });

                if (post.Reply.User_Id != request.Account_Id)
                {
                    return new Exception("Permission denied");
                }

                await _context.DeleteReplyAsync(request);
            }

            return "Reply deleted successfully";
        }
    }
    public class DeleteReplyCommandValidator : AbstractValidator<DeleteReplyCommand>
    {
        public DeleteReplyCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
