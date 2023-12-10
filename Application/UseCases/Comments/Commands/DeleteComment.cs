using Application.Common.Exceptions;
using Application.Common.Interfaces;
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

namespace Application.UseCases.Comments.Commands
{
    public class DeleteCommentCommand : IRequest
    {
        public int Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _context;

        public DeleteCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role == "Admin" || request.Account_Role == "Moderator")
            {
                await _context.DeleteCommentAsync(request);
            }
            else
            {
                var post = await _context.GetCommentByIdAsync(new Queries.GetCommentByIdQuery() { Id = request.Id });

                if (post.Comment.User_Id != request.Account_Id)
                {
                    throw new PermissionException();
                }

                await _context.DeleteCommentAsync(request);
            }
        }
    }
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
