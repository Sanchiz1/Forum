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
    public class GetUserByUsernameQuery : IRequest<Result<UserViewModel>>
    {
        public string Username { get; set; }
    }
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, Result<UserViewModel>>
    {
        private readonly IUserRepository _context;

        public GetUserByUsernameQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<UserViewModel>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken) => await _context.GetUserByUsernameAsync(request);
    }
}
