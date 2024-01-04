using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries
{
    public class GetSearchedUsersQuery : IRequest<Result<List<UserViewModel>>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Search { get; set; } = "%";
    }
    public class GetSearchedUsersQueryHandler : IRequestHandler<GetSearchedUsersQuery, Result<List<UserViewModel>>>
    {
        private readonly IUserRepository _context;

        public GetSearchedUsersQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<List<UserViewModel>>> Handle(GetSearchedUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetSearchedUsersAsync(request);
        }
    }
}
