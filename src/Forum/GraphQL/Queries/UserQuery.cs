using Forum.GraphQL.Types.UserTypes;
using GraphQL.Types;
using GraphQL;
using MediatR;
using Forum.Helpers;
using Application.UseCases.Users.Queries;

namespace Forum.GraphQL.Queries
{
    public class UserQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        public UserQuery(IMediator mediator)
        {
            _mediator = mediator;

            Field<ListGraphType<UserGraphType>>("searchedUsers")
                .Argument<NonNullGraphType<GetSearchedUsersInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetSearchedUsersQuery query = context.GetArgument<GetSearchedUsersQuery>("input");
                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<UserGraphType>("userById")
                .Argument<NonNullGraphType<GetUserByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetUserByIdQuery query = context.GetArgument<GetUserByIdQuery>("input");
                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<UserGraphType>("userByUsername")
                .Argument<NonNullGraphType<GetUserByUsernameInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetUserByUsernameQuery query = context.GetArgument<GetUserByUsernameQuery>("input");
                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<UserGraphType>("account")
                .ResolveAsync(async context =>
                {
                    GetUserByIdQuery query = new GetUserByIdQuery()
                    {
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
