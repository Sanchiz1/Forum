using Application.Common.ViewModels;
using Domain.Entities;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CommentTypes
{
    public class CommentGraphType : ObjectGraphType<CommentViewModel>
    {
        public CommentGraphType()
        {
            Field(i => i.Comment.Id, type: typeof(IntGraphType));
            Field(i => i.Comment.Text, type: typeof(StringGraphType));
            Field(i => i.Comment.Date, type: typeof(DateTimeGraphType));
            Field(i => i.Comment.Post_Id, type: typeof(IntGraphType));
            Field(i => i.Comment.User_Id, type: typeof(IntGraphType));
            Field(i => i.User_Username, type: typeof(StringGraphType));
            Field(i => i.Likes, type: typeof(IntGraphType));
            Field(i => i.Replies, type: typeof(IntGraphType));
            Field(i => i.Liked, type: typeof(BooleanGraphType));
        }
    }
}
