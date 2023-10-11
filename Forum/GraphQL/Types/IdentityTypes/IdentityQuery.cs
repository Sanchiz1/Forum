using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.IdentityTypes;
using Forum.Models;
using Forum.Services.Interfaces;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.WebUtilities;
namespace TimeTracker.GraphQL.Types.IdentityTypes
{
    public class IdentityQuery : ObjectGraphType
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenFactory _tokenFactory;


        public IdentityQuery(IConfiguration configuration, IUserRepository userRepository, ITokenRepository tokenRepository, ITokenFactory tokenFactory)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenFactory = tokenFactory;

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
                    throw new Exception("User does not exist");
                }

                var encodedJwt = _tokenFactory.GetAccessToken(user.Id);

                var refreshToken = _tokenFactory.GetRefreshToken(user.Id);
                _tokenRepository.CreateRefreshToken(refreshToken, user.Id);

                var response = new LoginOutput()
                {
                    access_token = encodedJwt,
                    user_id = user.Id,
                    refresh_token = refreshToken,
                };

                return response;
            });
            

            Field<StringGraphType>("logout").
              Resolve((context) =>
              {
                  HttpContext httpContext = context.RequestServices!.GetService<IHttpContextAccessor>()!.HttpContext!;
                  var refreshToken = httpContext.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                  _tokenRepository.DeleteRefreshToken(refreshToken);

                  return "Successfully";
              });
        }

        public LoginOutput ExpiredSessionError(IResolveFieldContext<object?> context)
        {
            context.Errors.Add(new ExecutionError("User does not auth"));
            return new LoginOutput()
            {
                access_token = new("",new DateTime(),new DateTime()),
                user_id = 0,
                refresh_token = new("Your session was expired. Please, login again", new DateTime(), new DateTime()),
            };
        }
    }
}
