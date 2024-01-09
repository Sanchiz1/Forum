using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries
{
    public class GetSearchedUsersQuery : IRequest<Result<List<UserViewModelDto>>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Search { get; set; } = "%";
    }
    public class GetSearchedUsersQueryHandler : IRequestHandler<GetSearchedUsersQuery, Result<List<UserViewModelDto>>>
    {
        private readonly IUserRepository _context;
        private readonly IMapper _mapper;

        public GetSearchedUsersQueryHandler(IUserRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<UserViewModelDto>>> Handle(GetSearchedUsersQuery request, CancellationToken cancellationToken)
            => _mapper.Map<List<UserViewModelDto>>(await _context.GetSearchedUsersAsync(request));
    }
}
