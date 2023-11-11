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
    public record UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _context;

        public UpdatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken) => await _context.UpdatePostAsync(request);
    }
}