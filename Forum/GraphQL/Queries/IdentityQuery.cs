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
        public IdentityQuery(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;

            Field<LoginResponseGraphType>("login")
                .Argument<NonNullGraphType<LoginInputGraphType>>("input")
            .ResolveAsync(async context =>
            {
                return await _mediator.Send(context.GetArgument<LoginQuery>("input"));
            });

            Field<LoginResponseGraphType>("refreshToken")
              .ResolveAsync(async context =>
              {
                    var refreshToken = _contextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;
                    
                    return await _mediator.Send(new RefreshTokenQuery() { Token = refreshToken});
                });

            Field<StringGraphType>("logout")
              .ResolveAsync(async context =>
              {
                  var refreshToken = _contextAccessor.HttpContext!.Request.Headers.First(at => at.Key == "refresh_token").Value[0]!;

                  await _mediator.Send(new LogoutQuery() { Token = refreshToken });

                  return "Successfully";
              });
        }
    }
}
