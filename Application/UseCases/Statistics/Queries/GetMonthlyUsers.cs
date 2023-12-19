using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Statistics.Queries
{
    public class GetMonthlyUsersQuery : IRequest<int[]>
    {
        public int Year { get; set; }
    }
    public class GetMonthlyUsersQueryHandler : IRequestHandler<GetMonthlyUsersQuery, int[]>
    {
        private readonly IStatisticsRepository _context;

        public GetMonthlyUsersQueryHandler(IStatisticsRepository context)
        {
            _context = context;
        }

        public async Task<int[]> Handle(GetMonthlyUsersQuery request, CancellationToken cancellationToken) => await _context.GetMonthlyUsersAsync(request);
    }
}
