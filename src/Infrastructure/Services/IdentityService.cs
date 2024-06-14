using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.UseCases.Identity.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenFactory _tokenFactory;
        private readonly ITokenValidator _tokenValidator;
        private readonly IHashingService _hasher;


        public IdentityService(IConfiguration configuration, 
            IMediator mediator,
            IUserRepository userRepository,
            ITokenRepository tokenRepository,
            ITokenFactory tokenFactory,
            ITokenValidator tokenValidator,
            IHashingService hasher)
        {
            _configuration = configuration;
            _mediator = mediator;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenFactory = tokenFactory;
            _tokenValidator = tokenValidator;
            _hasher = hasher;
        }

        public async Task<Result<LoginResponse>> Login(LoginQuery loginQuery)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(loginQuery.Username_Or_Email);

            if (user == null)
            {
                return new Exception("Wrong username or email");
            }
            string userSalt = await _userRepository.GetUserSaltAsync(user.User.Id);
            string userPassword = _hasher.ComputeHash(loginQuery.Password, userSalt);

            if (userPassword != await _userRepository.GetUserPasswordAsync(user.User.Id))
            {
                return new Exception("Wrong username or email");
            }

            var encodedJwt = _tokenFactory.GetAccessToken(user.User.Id, user.Role);

            var refreshToken = _tokenFactory.GetRefreshToken(user.User.Id);

            await _tokenRepository.CreateRefreshTokenAsync(refreshToken, user.User.Id);

            return new LoginResponse()
            {
                Access_Token = encodedJwt,
                User_Id = user.User.Id,
                Refresh_Token = refreshToken,
            };
        }
        public async Task<Result<LoginResponse>> RefreshToken(RefreshTokenQuery refreshTokenQuery)
        {
            if (refreshTokenQuery.Token == null || !(await _tokenValidator.ValidateRefreshTokenAsync(refreshTokenQuery.Token)))
            {
                return new Exception("Invalid token");
            }

            int userId = int.Parse(_tokenValidator.ReadJwtToken(refreshTokenQuery.Token).Claims.First(c => c.Type == "UserId").Value);

            var user = await _userRepository.GetUserByIdAsync(userId);

            var newAccessToken = _tokenFactory.GetAccessToken(user.User.Id, user.Role);
            var newRefreshToken = _tokenFactory.GetRefreshToken(user.User.Id);

            await _tokenRepository.UpdateRefreshTokenAsync(refreshTokenQuery.Token, newRefreshToken, userId);

            return new LoginResponse()
            {
                Access_Token = newAccessToken,
                User_Id = userId,
                Refresh_Token = newRefreshToken,
            };
        }
        public async Task Logout(LogoutQuery logoutQuery)
        {
            await _tokenRepository.DeleteRefreshTokenAsync(logoutQuery.Token);
        }
    }
}
