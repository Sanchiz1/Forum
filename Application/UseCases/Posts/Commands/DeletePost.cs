using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Application.Common.Interfaces;
using System.Runtime;
using System.Runtime.CompilerServices;
using Application;
using Application.UseCases;
using Application.UseCases.Posts;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Commands;
using Application.Common.Interfaces.Repositories;

namespace Application.UseCases.Posts.Commands
{
    public record DeletePostCommand : IRequest
    {
        public int Id {  get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _context;

        public DeletePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken) => await _context.DeletePostAsync(request);
    }
}