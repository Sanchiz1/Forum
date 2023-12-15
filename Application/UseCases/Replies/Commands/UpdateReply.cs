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
    public class UpdateReplyCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class UpdateReplyCommandHandler : IRequestHandler<UpdateReplyCommand>
    {
        private readonly IReplyRepository _context;

        public UpdateReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateReplyCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.GetReplyByIdAsync(new Queries.GetReplyByIdQuery() { Id = request.Id });

            if (post.Reply.User_Id != request.Account_Id)
            {
                throw new PermissionException();
            }

            await _context.UpdateReplyAsync(request);
        }
    }
    public class UpdateReplyCommandValidator : AbstractValidator<UpdateReplyCommand>
    {
        public UpdateReplyCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Text)
                .MaximumLength(1000)
                .NotEmpty();
        }
    }
}
