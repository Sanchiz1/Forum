using Application.Common.Exceptions;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Identity.Queries;
using Forum.GraphQL.Types.IdentityTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Queries
{
    public class IdentityQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger _logger;
        public IdentityQuery(IMediator mediator, IHttpContextAccessor contextAccessor, ILogger logger)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
            _logger = logger;

            Field<LoginResponseGraphType>("login")
                .Argument<NonNullGraphType<LoginInputGraphType>>("input")
            .ResolveAsync(async context =>
            {
                try
                {
                    return await _mediator.Send(context.GetArgument<LoginQuery>("input"));
                }
                catch (FailedLoginException)
                {
                    context.Errors.Add(new ExecutionError("Wrong username or password, try again"));
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Logging in");
                    context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                    return null;
                }
            });

            Field<LoginResponseGraphType>("refreshToken")
              .ResolveAsync(async context =>
              {
                  var refreshToken = _contextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                  try
                  {
                      return await _mediator.Send(new RefreshTokenQuery() { Token = refreshToken });
                  }
                  catch (InvalidTokenException)
                  {
                      context.Errors.Add(new ExecutionError("Invalid token"));
                      return null;
                  }
                  catch (Exception ex)
                  {
                      _logger.LogError(ex, "Refreshing token");
                      context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                      return null;
                  }

              });

            Field<StringGraphType>("logout")
              .ResolveAsync(async context =>
              {
                  var refreshToken = _contextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                  try
                  {
                      await _mediator.Send(new LogoutQuery() { Token = refreshToken });

                      return "Successfully";
                  }
                  catch (Exception ex)
                  {
                      _logger.LogError(ex, "Logging out");
                      context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                      return null;
                  }
              });
        }
    }
}
