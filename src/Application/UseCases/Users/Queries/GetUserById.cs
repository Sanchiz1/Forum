using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserViewModelDto>>
    {
        public int User_Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserViewModelDto>>
    {
        private readonly IUserRepository _context;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UserViewModelDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            => _mapper.Map<UserViewModelDto>(await _context.GetUserByIdAsync(request.User_Id));
    }
}
