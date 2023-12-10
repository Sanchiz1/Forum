using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Mutations
{
    public class MainMutation : ObjectGraphType
    {
        public MainMutation()
        {
            Field<UserMutation>("user")
            .Resolve(context => new { });
            Field<PostMutation>("post")
            .Resolve(context => new { }).AuthorizeWithPolicy("Authorized");
            Field<CommentMutation>("comment")
            .Resolve(context => new { }).AuthorizeWithPolicy("Authorized");
            Field<ReplyMutation>("reply")
            .Resolve(context => new { }).AuthorizeWithPolicy("Authorized");
            Field<CategoryMutation>("category")
            .Resolve(context => new { }).AuthorizeWithPolicy("AdminPolicy");
        }
    }
}
