using Forum.Data.Repositories.Interfaces;
using Forum.Helpers;
using Forum.Models;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserRepository repo;

        public UserMutation(IUserRepository Repo)
        {
            repo = Repo;

            Field<StringGraphType>("createUser")
                .Argument<NonNullGraphType<UserInputGraphType>>("User")
                .ResolveAsync(async context =>
                {
                    UserInput user = context.GetArgument<UserInput>("User");

                    if (!UserValidateHelper.ValidateUserCreateInput(user))
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }

                    if(repo.GetUserByUsername(user.Username) != null)
                    {
                        context.Errors.Add(new ExecutionError("User with this username already exists"));
                        return null;
                    }

                    if (repo.GetUserByEmail(user.Email) != null)
                    {
                        context.Errors.Add(new ExecutionError("User with this email already exists"));
                        return null;
                    }

                    repo.CreateUser(user);
                    return "Account created successfully";
                });

            Field<StringGraphType>("updateUser")
                .Argument<NonNullGraphType<UserInputGraphType>>("User")
                .ResolveAsync(async context =>
                {
                    UserInput user = context.GetArgument<UserInput>("User");

                    var userId = AccountHelper.GetUserIdFromClaims(context.User!);

                    if (!UserValidateHelper.ValidateUserUpdateInput(user))
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    try { 
                    var checkUsernameUser = repo.GetUserByUsername(user.Username);
                    var checkEmailUser = repo.GetUserByEmail(user.Email);

                    if (checkUsernameUser != null)
                    {
                        if(checkUsernameUser.Id != userId)
                        {
                            context.Errors.Add(new ExecutionError("User with this username already exists"));
                            return null;
                        }
                    }

                    if (checkEmailUser != null)
                    {
                        if(checkEmailUser.Id != userId)
                        {
                            context.Errors.Add(new ExecutionError("User with this email already exists"));
                            return null;
                        }
                    }

                    repo.UpdateUser(user, userId);
                    return "Account updated successfully";
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    return null;
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
