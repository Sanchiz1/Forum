﻿using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;

namespace Application.UseCases.Posts.Commands
{
    public class LikePostCommand : IRequest
    {
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand>
    {
        private readonly IPostRepository _context;

        public LikePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(LikePostCommand request, CancellationToken cancellationToken) => await _context.LikePostAsync(request);
    }
}