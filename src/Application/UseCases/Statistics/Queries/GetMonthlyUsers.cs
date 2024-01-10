using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Statistics.Queries
{
    public class GetMonthlyUsersQuery : IRequest<Result<int[]>>
    {
        public int Year { get; set; }
    }
    public class GetMonthlyUsersQueryHandler : IRequestHandler<GetMonthlyUsersQuery, Result<int[]>>
    {
        private readonly IStatisticsRepository _context;

        public GetMonthlyUsersQueryHandler(IStatisticsRepository context)
        {
            _context = context;
        }

        public async Task<Result<int[]>> Handle(GetMonthlyUsersQuery request, CancellationToken cancellationToken) => await _context.GetMonthlyUsersAsync(request);
    }
}
