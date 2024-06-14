using Api.GraphQL.Types.UserTypes;
using Api.Helpers;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Users.Commands;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Api.GraphQL.Mutations
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
                    CreateUserCommand command = context.GetArgument<CreateUserCommand>("input");

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateUser")
                .Argument<NonNullGraphType<UpdateUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserCommand command = context.GetArgument<UpdateUserCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));

                }).AuthorizeWithPolicy("Authorized");

            Field<StringGraphType>("updateUserRole")
                .Argument<NonNullGraphType<UpdateUserRoleInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserRoleCommand command = context.GetArgument<UpdateUserRoleCommand>("input");

                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("AdminPolicy");

            Field<StringGraphType>("deleteUser")
                .Argument<NonNullGraphType<DeleteUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteUserCommand command = context.GetArgument<DeleteUserCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("Authorized");


            Field<StringGraphType>("changeUserPassword")
                .Argument<NonNullGraphType<ChangeUserPasswordInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    ChangeUserPasswordCommand command = context.GetArgument<ChangeUserPasswordCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
