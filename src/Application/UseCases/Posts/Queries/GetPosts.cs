using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Queries
{
    public class GetPostsQuery : IRequest<Result<List<PostViewModelDto>>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public int User_Id { get; set; } = 0;
        public string Order { get; set; } = "Date";
        public int[] Categories { get; set; } = [];
    }
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, Result<List<PostViewModelDto>>>
    {
        private readonly IPostRepository _context;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IPostRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<PostViewModelDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<List<PostViewModelDto>>(await _context.GetPostsAsync(request));
    }
}
