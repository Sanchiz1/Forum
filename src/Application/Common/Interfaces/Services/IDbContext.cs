using System.Data;

namespace Application.Common.Interfaces.Services
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
