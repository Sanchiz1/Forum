using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IHashingService
    {
        string ComputeHash(string password, string salt);

        string GenerateSalt();
    }
}
