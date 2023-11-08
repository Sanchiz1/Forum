using Forum.Models.Comments;
using Forum.Models.Replies;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CommentTypes
{
    public class CommentInputGraphType : InputObjectGraphType<CommentInput>
    {
        public CommentInputGraphType()
        {
            Field(i => i.Text, type: typeof(StringGraphType));
            Field(i => i.Post_Id, type: typeof(IntGraphType));
            Field(i => i.User_Id, type: typeof(IntGraphType));
        }
    }
}
