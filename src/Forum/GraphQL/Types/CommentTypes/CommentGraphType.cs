using Application.Common.DTOs.ViewModels;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CommentTypes
{
    public class CommentGraphType : ObjectGraphType<CommentViewModelDto>
    {
        public CommentGraphType()
        {
            Field(i => i.Comment.Id, type: typeof(IntGraphType));
            Field(i => i.Comment.Text, type: typeof(StringGraphType));
            Field(i => i.Comment.Date_Created, type: typeof(DateTimeGraphType));
            Field(i => i.Comment.Date_Edited, type: typeof(DateTimeGraphType), nullable: true);
            Field(i => i.Comment.Post_Id, type: typeof(IntGraphType));
            Field(i => i.Comment.User_Id, type: typeof(IntGraphType));
            Field(i => i.User_Username, type: typeof(StringGraphType));
            Field(i => i.Likes, type: typeof(IntGraphType));
            Field(i => i.Replies, type: typeof(IntGraphType));
            Field(i => i.Liked, type: typeof(BooleanGraphType));
        }
    }
}
