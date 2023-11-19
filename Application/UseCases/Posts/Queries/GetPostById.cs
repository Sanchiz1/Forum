using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
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
    public class GetPostByIdQuery : IRequest<PostViewModel>
    {
        public int Id { get; set; }
        public int User_id { get; set; } = 0;
    }
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostViewModel>
    {
        private readonly IPostRepository _context;

        public GetPostByIdQueryHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<PostViewModel> Handle(GetPostByIdQuery request, CancellationToken cancellationToken) => await _context.GetPostByIdAsync(request);
    }
}
