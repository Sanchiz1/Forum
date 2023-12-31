﻿using Application.Common.Interfaces.Repositories;
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
    public class GetUserByIdQuery : IRequest<Result<UserViewModel>>
    {
        public int User_Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserViewModel>>
    {
        private readonly IUserRepository _context;

        public GetUserByIdQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) => await _context.GetUserByIdAsync(request.User_Id);
    }
}
