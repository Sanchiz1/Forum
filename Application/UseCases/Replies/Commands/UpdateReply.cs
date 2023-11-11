using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Commands
{
    public record UpdateReplyCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class UpdateReplyCommandHandler : IRequestHandler<UpdateReplyCommand>
    {
        private readonly IReplyRepository _context;

        public UpdateReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateReplyCommand request, CancellationToken cancellationToken) => await _context.UpdateReplyAsync(request);
    }
}
