using Forum.Models.Posts;
using Forum.Models.User;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class PostGraphType : ObjectGraphType<Post>
    {
        public PostGraphType()
        {
            Field(i => i.Id, type: typeof(IntGraphType));
            Field(i => i.Title, type: typeof(StringGraphType));
            Field(i => i.Text, type: typeof(StringGraphType));
            Field(i => i.Date, type: typeof(DateTimeGraphType));
            Field(i => i.User_Id, type: typeof(IntGraphType));
        }
    }
}
