using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Queries
{
    public record GetPostByIdQuery : IRequest<Post>
    {
        public int Id { get; set; }
        public int User_id = 0;
    }
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly IPostRepository _context;

        public GetPostByIdQueryHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken) => await _context.GetPostByIdAsync(request);
    }
}
