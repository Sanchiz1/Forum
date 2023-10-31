using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.PostTypes;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using Forum.Models;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types
{
    public class MainMutation : ObjectGraphType
    {
        public MainMutation()
        {
            Field<UserMutation>("user")
            .Resolve(context => new { });
            Field<PostMutation>("post")
            .Resolve(context => new { });
        }
    }
}
