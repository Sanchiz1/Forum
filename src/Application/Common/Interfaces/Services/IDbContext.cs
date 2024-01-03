using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;

namespace Application.Common.Interfaces.Services
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
