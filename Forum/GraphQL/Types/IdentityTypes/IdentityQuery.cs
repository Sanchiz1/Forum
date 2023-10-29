using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.IdentityTypes;
using Forum.Models.Identity;
using Forum.Services.Interfaces;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.GraphQL.Types.IdentityTypes
{
    public class IdentityQuery : ObjectGraphType
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenFactory _tokenFactory;
        private readonly ITokenValidator _tokenValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityQuery(
            IConfiguration configuration,
            IUserRepository userRepository,
            ITokenRepository tokenRepository,
            ITokenFactory tokenFactory,
            ITokenValidator tokenValidator,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenFactory = tokenFactory;
            _tokenValidator = tokenValidator;
            _httpContextAccessor = httpContextAccessor;

            Field<LoginOutputGraphType>("login")
                .Argument<NonNullGraphType<CredentialsInputGraphType>>("login")
            .Resolve(context =>
            {
                Credentials UserLogData = context.GetArgument<Credentials>("login");
                var userRepository = context.RequestServices.GetService<IUserRepository>();
                var config = context.RequestServices.GetService<IConfiguration>();

                var user = userRepository.GetUserByCredentials(UserLogData.LoginOrEmail, UserLogData.Password);


                if (user == null)
                {
                    context.Errors.Add(new ExecutionError("Wrong username or password, try again"));
                    return null;
                }

                var encodedJwt = _tokenFactory.GetAccessToken(user.Id);

                var refreshToken = _tokenFactory.GetRefreshToken(user.Id);
                _tokenRepository.DeleteAllRefreshTokens(user.Id);
                _tokenRepository.CreateRefreshToken(refreshToken, user.Id);

                var response = new LoginOutput()
                {
                    access_token = encodedJwt,
                    user_id = user.Id,
                    refresh_token = refreshToken,
                };

                return response;
            });

            Field<LoginOutputGraphType>("refreshToken").
                Resolve((context) =>
                {
                    var refreshToken = _httpContextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                    if (refreshToken == null)
                    {
                        return ExpiredSessionError(context);
                    }

                    try
                    {
                        _tokenValidator.ValidateRefreshToken(refreshToken);
                    }
                    catch (ValidationException ex)
                    {
                        tokenRepository.DeleteRefreshToken(refreshToken);
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }

                    int userId = int.Parse(_tokenValidator.ReadJwtToken(refreshToken).Claims.First(c => c.Type == "UserId").Value);

                    var newAccessToken = tokenFactory.GetAccessToken(userId);
                    var newRefreshToken = tokenFactory.GetRefreshToken(userId);

                    _tokenRepository.UpdateRefreshToken(refreshToken, newRefreshToken, userId);

                    return new LoginOutput()
                    {
                        access_token = newAccessToken,
                        user_id = userId,
                        refresh_token=newRefreshToken,
                    };
                });

            Field<StringGraphType>("logout").
              Resolve((context) =>
              {
                  var refreshToken = _httpContextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                  _tokenRepository.DeleteRefreshToken(refreshToken);

                  return "Successfully";
              });
        }

        public LoginOutput ExpiredSessionError(IResolveFieldContext<object?> context)
        {
            context.Errors.Add(new ExecutionError("User does not auth"));
            return new LoginOutput()
            {
                access_token = new("", new DateTime(), new DateTime()),
                user_id = 0,
                refresh_token = new("Your session was expired. Please, login again", new DateTime(), new DateTime()),
            };
        }
    }
}
