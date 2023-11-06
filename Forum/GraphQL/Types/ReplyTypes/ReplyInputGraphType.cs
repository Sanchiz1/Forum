using Forum.Models.Posts;
using Forum.Models.Replies;
using GraphQL.Types;

namespace Forum.GraphQL.Types.ReplyTypes
{
    public class ReplyInputGraphType : InputObjectGraphType<ReplyInput>
    {
        public ReplyInputGraphType()
        {
            Field(i => i.Text, type: typeof(StringGraphType));
            Field(i => i.Post_Id, type: typeof(IntGraphType));
            Field(i => i.Reply_Id, type: typeof(IntGraphType));
            Field(i => i.User_Id, type: typeof(IntGraphType));
            Field(i => i.User_Username, type: typeof(StringGraphType));
        }
    }
}
