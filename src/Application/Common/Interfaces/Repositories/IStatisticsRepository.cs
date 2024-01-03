using Application.Common.DTOs;
using Application.UseCases.Categories.Queries;
using Application.UseCases.Statistics.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IStatisticsRepository
    {
        Task<int[]> GetMonthlyPostsAsync(GetMonthlyPostsQuery getMonthlyPostsQuery);
        Task<int[]> GetMonthlyUsersAsync(GetMonthlyUsersQuery getMonthlyUsersQuery);
    }
}
