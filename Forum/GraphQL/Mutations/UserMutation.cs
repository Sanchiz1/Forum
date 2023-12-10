using Application.Common.Exceptions;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Users.Commands;
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
                    CreateUserCommand createUserCommand = context.GetArgument<CreateUserCommand>("input");

                    try
                    {
                        await _mediator.Send(createUserCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (UserAlreadyExistsException ex)
                    {
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }
                    catch
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }

                    return "Account created successfully";
                });

            Field<StringGraphType>("updateUser")
                .Argument<NonNullGraphType<UpdateUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserCommand updateUserCommand = new UpdateUserCommand()
                    {
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Username = context.GetArgument<UpdateUserCommand>("input").Username,
                        Email = context.GetArgument<UpdateUserCommand>("input").Email,
                        Bio = context.GetArgument<UpdateUserCommand>("input").Bio,
                    };

                    try
                    {
                        await _mediator.Send(updateUserCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (UserAlreadyExistsException ex)
                    {
                        context.Errors.Add(new ExecutionError(ex.Message));
                        return null;
                    }
                    catch
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }

                    return "Account updated successfully";
                }).AuthorizeWithPolicy("Authorized");

            Field<StringGraphType>("updateUserRole")
                .Argument<NonNullGraphType<UpdateUserRoleInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateUserRoleCommand updateUserRoleCommand = new UpdateUserRoleCommand()
                    {
                        User_Id = context.GetArgument<UpdateUserRoleCommand>("input").User_Id,
                        Role_Id = context.GetArgument<UpdateUserRoleCommand>("input").Role_Id,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(updateUserRoleCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }

                    return "Role updated successfully";
                }).AuthorizeWithPolicy("AdminPolicy");
        }
    }
}
