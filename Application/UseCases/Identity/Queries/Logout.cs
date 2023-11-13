﻿using Application.Common.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Identity.Queries
{
    public class LogoutQuery : IRequest
    {
        public string Token { get; set; }
    }
    public class LogoutQueryHandler : IRequestHandler<LogoutQuery>
    {
        private readonly IIdentityService _context;

        public LogoutQueryHandler(IIdentityService context)
        {
            _context = context;
        }

        public async Task Handle(LogoutQuery request, CancellationToken cancellationToken) => await _context.Logout(request);
    }
}