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
                    GetSearchedUsersQuery getSearchedUsersQuery = context.GetArgument<GetSearchedUsersQuery>("input");
                    return await _mediator.Send(getSearchedUsersQuery);
                });

            Field<UserGraphType>("userById")
                .Argument<NonNullGraphType<GetUserByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetUserByIdQuery getUserByIdQuery = context.GetArgument<GetUserByIdQuery>("input");
                    return await _mediator.Send(getUserByIdQuery);
                });

            Field<UserGraphType>("userByUsername")
                .Argument<NonNullGraphType<GetUserByUsernameInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetUserByUsernameQuery getUserByUsernameQuery = context.GetArgument<GetUserByUsernameQuery>("input");
                    return await _mediator.Send(getUserByUsernameQuery);
                });

            Field<UserGraphType>("account")
                .ResolveAsync(async context =>
                {
                    GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery()
                    {
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getUserByIdQuery);
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
