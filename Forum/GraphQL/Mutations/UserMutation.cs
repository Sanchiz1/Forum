using Application.Common.Exceptions;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using FluentValidation;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
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
                    CreateUserCommand command = context.GetArgument<CreateUserCommand>("input");

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateUser")
                .Argument<NonNullGraphType<UpdateUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserCommand command = new UpdateUserCommand()
                    {
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Username = context.GetArgument<UpdateUserCommand>("input").Username,
                        Email = context.GetArgument<UpdateUserCommand>("input").Email,
                        Bio = context.GetArgument<UpdateUserCommand>("input").Bio,
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));

                }).AuthorizeWithPolicy("Authorized");

            Field<StringGraphType>("updateUserRole")
                .Argument<NonNullGraphType<UpdateUserRoleInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserRoleCommand command = new UpdateUserRoleCommand()
                    {
                        User_Id = context.GetArgument<UpdateUserRoleCommand>("input").User_Id,
                        Role_Id = context.GetArgument<UpdateUserRoleCommand>("input").Role_Id,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("AdminPolicy");

            Field<StringGraphType>("deleteUser")
                .Argument<NonNullGraphType<DeleteUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteUserCommand command = new DeleteUserCommand()
                    {
                        User_Id = context.GetArgument<DeleteUserCommand>("input").User_Id,
                        Password = context.GetArgument<DeleteUserCommand>("input").Password,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("Authorized");


            Field<StringGraphType>("changeUserPassword")
                .Argument<NonNullGraphType<ChangeUserPasswordInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    ChangeUserPasswordCommand command = new ChangeUserPasswordCommand()
                    {
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Password = context.GetArgument<ChangeUserPasswordCommand>("input").Password,
                        New_Password = context.GetArgument<ChangeUserPasswordCommand>("input").New_Password,
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
