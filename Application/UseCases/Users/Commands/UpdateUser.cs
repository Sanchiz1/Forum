﻿using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? Password { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _context;

        public UpdateUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if ((await _context.GetUserByUsernameAsync(new GetUserByUsernameQuery() { Username = request.Username }))?.Id != request.User_Id)
            {
                throw new Exception("User with this username already exists");
            }

            if ((await _context.GetUserByEmailAsync(new GetUserByEmailQuery() { Email = request.Username }))?.Id != request.User_Id)
            {
                throw new Exception("User with this email already exists");
            }

            await _context.UpdateUserAsync(request);
        }
    }
}
