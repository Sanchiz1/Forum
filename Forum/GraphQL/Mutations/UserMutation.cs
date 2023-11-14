using Application.UseCases.Users.Commands;
using Forum.GraphQL.Types.UserTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IMediator _mediator;

        public UserMutation(IMediator mediator)
        {
            _mediator = mediator;

            Field<StringGraphType>("createUser")
                .Argument<NonNullGraphType<CreateUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    CreateUserCommand createUserCommand = context.GetArgument<CreateUserCommand>("input");

                    await _mediator.Send(createUserCommand);
                    return "Account created successfully";
                });

            Field<StringGraphType>("updateUser")
                .Argument<NonNullGraphType<UpdateUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserCommand updateUserCommand = context.GetArgument<UpdateUserCommand>("input");

                    await _mediator.Send(updateUserCommand);
                    return "Account updated successfully";
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
