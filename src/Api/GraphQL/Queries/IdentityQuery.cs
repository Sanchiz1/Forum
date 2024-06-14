using Api.GraphQL.Types.IdentityTypes;
using Application.UseCases.Identity.Queries;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Api.GraphQL.Queries
{
    public class IdentityQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        public IdentityQuery(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;

            Field<LoginResponseGraphType>("login")
                .Argument<NonNullGraphType<LoginInputGraphType>>("input")
            .ResolveAsync(async context =>
            {
                LoginQuery query = context.GetArgument<LoginQuery>("input");

                var result = await _mediator.Send(query);

                return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
            });

            Field<LoginResponseGraphType>("refreshToken")
              .ResolveAsync(async context =>
              {
                  var refreshToken = _contextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0];

                  RefreshTokenQuery query = new RefreshTokenQuery() { Token = refreshToken };

                  var result = await _mediator.Send(query);

                  return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
              });

            Field<StringGraphType>("logout")
              .ResolveAsync(async context =>
              {
                  var refreshToken = _contextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                  LogoutQuery query = new LogoutQuery() { Token = refreshToken };

                  var result = await _mediator.Send(query);

                  return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
              });
        }
    }
}
