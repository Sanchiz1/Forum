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
    public record CreatePostCommand : IRequest
    {
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostRepository _context;

        public CreatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken) => await _context.CreatePostAsync(request);
    }
}