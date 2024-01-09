using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<Result<UserViewModelDto>>
    {
        public string Email { get; set; }
    }
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserViewModelDto>>
    {
        private readonly IUserRepository _context;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(IUserRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UserViewModelDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
            => _mapper.Map<UserViewModelDto>(await _context.GetUserByEmailAsync(request));
    }
}
