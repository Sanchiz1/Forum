using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Statistics.Queries
{
    public class GetMonthlyPostsQuery : IRequest<int[]>
    {
        public int Year { get; set; }
    }
    public class GetMonthlyPostsQueryHandler : IRequestHandler<GetMonthlyPostsQuery, int[]>
    {
        private readonly IStatisticsRepository _context;

        public GetMonthlyPostsQueryHandler(IStatisticsRepository context)
        {
            _context = context;
        }

        public async Task<int[]> Handle(GetMonthlyPostsQuery request, CancellationToken cancellationToken) => await _context.GetMonthlyPostsAsync(request);
    }
}
