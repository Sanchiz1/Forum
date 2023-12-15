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

            Field<StringGraphType>("deleteUser")
                .Argument<NonNullGraphType<DeleteUserInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteUserCommand deleteUserCommand = new DeleteUserCommand()
                    {
                        User_Id = context.GetArgument<DeleteUserCommand>("input").User_Id,
                        Password = context.GetArgument<DeleteUserCommand>("input").Password,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(deleteUserCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
                    }
                    catch (WrongPasswordException ex)
                    {
                        context.Errors.Add(new ExecutionError("Wrong password"));
                        return null;
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
                }).AuthorizeWithPolicy("Authorized");


            Field<StringGraphType>("changeUserPassword")
                .Argument<NonNullGraphType<ChangeUserPasswordInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    ChangeUserPasswordCommand changeUserPasswordCommand = new ChangeUserPasswordCommand()
                    {
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Password = context.GetArgument<ChangeUserPasswordCommand>("input").Password,
                        New_Password = context.GetArgument<ChangeUserPasswordCommand>("input").New_Password,
                    };
                    try
                    {
                        await _mediator.Send(changeUserPasswordCommand);
                    }
                    catch (WrongPasswordException ex)
                    {
                        context.Errors.Add(new ExecutionError("Wrong password"));
                        return null;
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

                    return "Password changed successfully";
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
