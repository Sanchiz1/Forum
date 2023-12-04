using Application.Common.DTOs;
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
    public class GetSearchedPostsQuery : IRequest<List<PostViewModel>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public int User_Id { get; set; } = 0;
        public string Order { get; set; } = "Date";
        public string Search { get; set; } = "%";
    }
    public class GetSearchedPostsQueryHandler : IRequestHandler<GetSearchedPostsQuery, List<PostViewModel>>
    {
        private readonly IPostRepository _context;

        public GetSearchedPostsQueryHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<List<PostViewModel>> Handle(GetSearchedPostsQuery request, CancellationToken cancellationToken) => await _context.GetSearchedPostsAsync(request);
    }
}
