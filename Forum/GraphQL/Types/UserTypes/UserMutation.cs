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

                    if (!UserValidateHelper.ValidateUserInput(user))
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
        }
    }
}
