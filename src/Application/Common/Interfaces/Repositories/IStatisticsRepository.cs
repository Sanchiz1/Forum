using Application.UseCases.Statistics.Queries;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IStatisticsRepository
    {
        Task<int[]> GetMonthlyPostsAsync(GetMonthlyPostsQuery getMonthlyPostsQuery);
        Task<int[]> GetMonthlyUsersAsync(GetMonthlyUsersQuery getMonthlyUsersQuery);
    }
}
