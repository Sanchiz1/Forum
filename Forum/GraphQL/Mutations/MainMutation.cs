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
            .Resolve(context => new { });
            Field<CommentMutation>("comment")
            .Resolve(context => new { });
            Field<ReplyMutation>("reply")
            .Resolve(context => new { });
        }
    }
}
