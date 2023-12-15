using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
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
    public class CreateReplyCommand : IRequest
    {
        public string Text { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_User_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand>
    {
        private readonly IReplyRepository _context;

        public CreateReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateReplyCommand request, CancellationToken cancellationToken)
        {
            if (request.User_Id == 0)
            {
                throw new PermissionException();
            }
            await _context.CreateReplyAsync(request);
        }
    }
    public class CreateReplyCommandValidator : AbstractValidator<CreateReplyCommand>
    {
        public CreateReplyCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(1000)
                .NotEmpty();
            RuleFor(c => c.Comment_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
