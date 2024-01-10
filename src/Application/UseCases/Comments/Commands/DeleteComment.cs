using Application.Common.Constants;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Commands
{
    public class DeleteCommentCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<string>>
    {
        private readonly ICommentRepository _context;

        public DeleteCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role == Role.Admin || request.Account_Role == Role.Moderator)
            {
                await _context.DeleteCommentAsync(request);
            }
            else
            {
                var post = await _context.GetCommentByIdAsync(new Queries.GetCommentByIdQuery() { Id = request.Id });

                if (post.Comment.User_Id != request.Account_Id)
                {
                    return new Exception("Permission denied");
                }

                await _context.DeleteCommentAsync(request);
            }

            return "Comment deleted successfully";
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
