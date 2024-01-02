using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
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
    public class GetUserByEmailQuery : IRequest<Result<UserViewModel>>
    {
        public string Email { get; set; }
    }
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserViewModel>>
    {
        private readonly IUserRepository _context;

        public GetUserByEmailQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<UserViewModel>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) => await _context.GetUserByEmailAsync(request);
    }
}
