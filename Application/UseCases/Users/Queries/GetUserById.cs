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

namespace Application.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public int User_Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IUserRepository _context;

        public GetUserByIdQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) => await _context.GetUserByIdAsync(request);
    }
}
