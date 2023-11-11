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
    public record LikeReplyCommand : IRequest
    {
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class LikeReplyCommandHandler : IRequestHandler<LikeReplyCommand>
    {
        private readonly IReplyRepository _context;

        public LikeReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task Handle(LikeReplyCommand request, CancellationToken cancellationToken) => await _context.LikeReplyAsync(request);
    }
}
