using Application.Common.ViewModels;
using Domain.Entities;
using GraphQL.Types;

namespace Forum.GraphQL.Types.ReplyTypes
{
    public class ReplyGraphType : ObjectGraphType<ReplyViewModel>
    {
        public ReplyGraphType()
        {
            Field(i => i.Reply.Id, type: typeof(IntGraphType));
            Field(i => i.Reply.Text, type: typeof(StringGraphType));
            Field(i => i.Reply.Date_Created, type: typeof(DateTimeGraphType));
            Field(i => i.Reply.Date_Edited, type: typeof(DateTimeGraphType), nullable: true);
            Field(i => i.Reply.Comment_Id, type: typeof(IntGraphType));
            Field(i => i.Reply.Reply_User_Id, type: typeof(IntGraphType));
            Field(i => i.Reply.User_Id, type: typeof(IntGraphType), nullable: true);
            Field(i => i.User_Username, type: typeof(StringGraphType));
            Field(i => i.Reply_Username, type: typeof(StringGraphType), nullable: true);
            Field(i => i.Likes, type: typeof(IntGraphType));
            Field(i => i.Liked, type: typeof(BooleanGraphType));
            Field(i => i.Is_Deleted, type: typeof(BooleanGraphType));
        }
    }
}
