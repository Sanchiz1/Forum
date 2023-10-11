using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using Forum.Models;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types
{
    public class MainQuery : ObjectGraphType
    {
        private readonly IUserRepository repo;
        public MainQuery(IUserRepository Repo)
        {
            repo = Repo;
            Field<UserGraphType>("exampleUser")
                .ResolveAsync(async context =>
                {
                    return new User("ExampleName", "exampleemail@gmail.com", "Example bio", "examplepaassword");
                });
            Field<ListGraphType<UserGraphType>>("users")
                .ResolveAsync(async context => repo.GetUsers());
            Field<UserGraphType>("user")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    return repo.GetUserById(id);
                });
        }
    }
}
