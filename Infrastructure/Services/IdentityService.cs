﻿using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Identity.Queries;
using Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
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


        public IdentityService(IConfiguration configuration, 
            IMediator mediator,
            IUserRepository userRepository,
            ITokenRepository tokenRepository,
            ITokenFactory tokenFactory,
            ITokenValidator tokenValidator)
        {
            _configuration = configuration;
            _mediator = mediator;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenFactory = tokenFactory;
            _tokenValidator = tokenValidator;
        }

        public async Task<LoginResponse> Login(LoginQuery loginQuery)
        {
            var user = (await _mediator.Send(new GetUserByCredentialsQuery { Username_Or_Email =  loginQuery.Username_Or_Email, Password = loginQuery.Password })).User;


            if (user == null) throw new FailedLoginException();

            var encodedJwt = _tokenFactory.GetAccessToken(user.Id);

            var refreshToken = _tokenFactory.GetRefreshToken(user.Id);

            await _tokenRepository.CreateRefreshTokenAsync(refreshToken, user.Id);

            return new LoginResponse()
            {
                Access_Token = encodedJwt,
                User_Id = user.Id,
                Refresh_Token = refreshToken,
            };
        }
        public async Task<LoginResponse> RefreshToken(RefreshTokenQuery refreshTokenQuery)
        {
            if (refreshTokenQuery.Token == null || !_tokenValidator.ValidateRefreshToken(refreshTokenQuery.Token))
            {
                throw new InvalidTokenException();
            }

            int userId = int.Parse(_tokenValidator.ReadJwtToken(refreshTokenQuery.Token).Claims.First(c => c.Type == "UserId").Value);

            var newAccessToken = _tokenFactory.GetAccessToken(userId);
            var newRefreshToken = _tokenFactory.GetRefreshToken(userId);

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
