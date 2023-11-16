using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ITokenFactory
    {
        Token GetRefreshToken(int userId);
        Token GetAccessToken(int userId);
    }
}
