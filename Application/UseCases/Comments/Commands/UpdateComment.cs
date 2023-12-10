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
    public class UpdateCommentCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _context;

        public UpdateCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.GetCommentByIdAsync(new Queries.GetCommentByIdQuery() { Id = request.Id });

            if (post.Comment.User_Id != request.Account_Id)
            {
                throw new PermissionException();
            }

            await _context.UpdateCommentAsync(request);
        }
    }
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(500)
                .NotEmpty();
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
