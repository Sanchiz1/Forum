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
    public record CreateReplyCommand : IRequest
    {
        public string Text { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand>
    {
        private readonly IReplyRepository _context;

        public CreateReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateReplyCommand request, CancellationToken cancellationToken) => await _context.CreateReplyAsync(request);
    }
}
