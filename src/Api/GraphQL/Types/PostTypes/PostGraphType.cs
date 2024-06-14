using Api.GraphQL.Types.CategoryTypes;
using Application.Common.DTOs.ViewModels;
using GraphQL.Types;

namespace Api.GraphQL.Types.PostTypes
{
    public class PostGraphType : ObjectGraphType<PostViewModelDto>
    {
        public PostGraphType()
        {
            Field(i => i.Post.Id, type: typeof(IntGraphType));
            Field(i => i.Post.Title, type: typeof(StringGraphType));
            Field(i => i.Post.Text, type: typeof(StringGraphType), nullable: true);
            Field(i => i.Post.Date_Created, type: typeof(DateTimeGraphType));
            Field(i => i.Post.Date_Edited, type: typeof(DateTimeGraphType), nullable: true);
            Field(i => i.Post.User_Id, type: typeof(IntGraphType));
            Field(i => i.Categories, type: typeof(ListGraphType<CategoryGraphType>));
            Field(i => i.User_Username, type: typeof(StringGraphType));
            Field(i => i.Likes, type: typeof(IntGraphType));
            Field(i => i.Comments, type: typeof(IntGraphType));
            Field(i => i.Liked, type: typeof(BooleanGraphType));
        }
    }
}
