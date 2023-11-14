﻿using Application.Common.Interfaces.Repositories;
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
    public class GetPostsQuery : IRequest<List<Post>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Order { get; set; } = "Date";
        public int User_Id { get; set; } = 0;
    }
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<Post>>
    {
        private readonly IPostRepository _context;

        public GetPostsQueryHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<List<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken) => await _context.GetPostsAsync(request);
    }
}
