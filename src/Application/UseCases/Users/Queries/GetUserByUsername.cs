using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<Result<UserViewModelDto>>
    {
        public string Username { get; set; }
    }
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, Result<UserViewModelDto>>
    {
        private readonly IUserRepository _context;
        private readonly IMapper _mapper;

        public GetUserByUsernameQueryHandler(IUserRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UserViewModelDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
            => _mapper.Map<UserViewModelDto>(await _context.GetUserByUsernameAsync(request));
    }
}
