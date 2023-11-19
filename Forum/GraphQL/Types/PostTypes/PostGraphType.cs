using Application.Common.ViewModels;
using Domain.Entities;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class PostGraphType : ObjectGraphType<PostViewModel>
    {
        public PostGraphType()
        {
            Field(i => i.Post.Id, type: typeof(IntGraphType));
            Field(i => i.Post.Title, type: typeof(StringGraphType));
            Field(i => i.Post.Text, type: typeof(StringGraphType), nullable: true);
            Field(i => i.Post.Date, type: typeof(DateTimeGraphType));
            Field(i => i.Post.User_Id, type: typeof(IntGraphType));
            Field(i => i.User_Username, type: typeof(StringGraphType));
            Field(i => i.Likes, type: typeof(IntGraphType));
            Field(i => i.Comments, type: typeof(IntGraphType));
            Field(i => i.Liked, type: typeof(BooleanGraphType));
        }
    }
}
