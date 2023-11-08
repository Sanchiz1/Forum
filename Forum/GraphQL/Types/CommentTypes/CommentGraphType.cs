using Forum.Models.Comments;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CommentTypes
{
    public class CommentGraphType : ObjectGraphType<Comment>
    {
        public CommentGraphType()
        {
            Field(i => i.Id, type: typeof(IntGraphType));
            Field(i => i.Text, type: typeof(StringGraphType));
            Field(i => i.Date, type: typeof(DateTimeGraphType));
            Field(i => i.Post_Id, type: typeof(IntGraphType));
            Field(i => i.User_Id, type: typeof(IntGraphType));
            Field(i => i.User_Username, type: typeof(StringGraphType));
        }
    }
}
