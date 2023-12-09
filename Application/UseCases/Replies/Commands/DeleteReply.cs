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
    }
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand>
    {
        private readonly IReplyRepository _context;

        public DeleteReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeleteReplyCommand request, CancellationToken cancellationToken) => await _context.DeleteReplyAsync(request);
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
