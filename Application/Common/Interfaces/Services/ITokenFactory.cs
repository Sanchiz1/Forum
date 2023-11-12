﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ITokenFactory
    {
        Task<Token> GetRefreshToken(int userId);
        Task<Token> GetAccessToken(int userId);
    }
}
