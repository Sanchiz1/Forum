using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Commands
{
    public class DeleteReplyCommand : IRequest
    {
        public int Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand>
    {
        private readonly IReplyRepository _context;

        public DeleteReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeleteReplyCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role == "Admin" || request.Account_Role == "Moderator")
            {
                await _context.DeleteReplyAsync(request);
            }
            else
            {
                var post = await _context.GetReplyByIdAsync(new Queries.GetReplyByIdQuery() { Id = request.Id });

                if (post.Reply.User_Id != request.Account_Id)
                {
                    throw new PermissionException();
                }

                await _context.DeleteReplyAsync(request);
            }
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
