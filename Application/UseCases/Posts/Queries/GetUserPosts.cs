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
    public class GetUserPostsQuery : IRequest<List<PostViewModel>>
    {
        public string Author_Username { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public int User_Id { get; set; } = 0;
        public string Order { get; set; } = "Date";
    }
    public class GetUserPostsQueryHandler : IRequestHandler<GetUserPostsQuery, List<PostViewModel>>
    {
        private readonly IPostRepository _context;

        public GetUserPostsQueryHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<List<PostViewModel>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken) => await _context.GetUserPostsAsync(request);
    }
}
