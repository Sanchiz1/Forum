using Forum.Models.Posts;
using Forum.Models.User;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class PostInputGraphType : InputObjectGraphType<PostInput>
    {
        public PostInputGraphType()
        {
            Field(i => i.Title, type: typeof(StringGraphType));
            Field(i => i.Text, type: typeof(StringGraphType), nullable: true);
            Field(i => i.User_Id, type: typeof(IntGraphType));
        }
    }
}
