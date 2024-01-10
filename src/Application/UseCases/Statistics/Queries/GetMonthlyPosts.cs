using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Statistics.Queries
{
    public class GetMonthlyPostsQuery : IRequest<Result<int[]>>
    {
        public int Year { get; set; }
    }
    public class GetMonthlyPostsQueryHandler : IRequestHandler<GetMonthlyPostsQuery, Result<int[]>>
    {
        private readonly IStatisticsRepository _context;

        public GetMonthlyPostsQueryHandler(IStatisticsRepository context)
        {
            _context = context;
        }

        public async Task<Result<int[]>> Handle(GetMonthlyPostsQuery request, CancellationToken cancellationToken) => await _context.GetMonthlyPostsAsync(request);
    }
}
