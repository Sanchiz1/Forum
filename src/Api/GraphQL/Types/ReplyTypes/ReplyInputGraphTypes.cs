using Application.UseCases.Replies.Commands;
using Application.UseCases.Replies.Queries;
using GraphQL.Types;

namespace Api.GraphQL.Types.ReplyTypes
{
    public class GetRepliesInputGraphType : InputObjectGraphType<GetRepliesQuery>
    {
        public GetRepliesInputGraphType()
        {
            Field(p => p.Comment_Id, type: typeof(IntGraphType));
            Field(p => p.Next, type: typeof(IntGraphType));
            Field(p => p.Offset, type: typeof(IntGraphType));
            Field(p => p.User_Timestamp, type: typeof(DateTimeGraphType));
            Field(p => p.Order, type: typeof(StringGraphType));
        }
    }
    public class GetReplyByIdInputGraphType : InputObjectGraphType<GetReplyByIdQuery>
    {
        public GetReplyByIdInputGraphType()
        {
            Field(p => p.Id, type: typeof(IntGraphType));
        }
    }
    public class CreateReplyInputGraphType : InputObjectGraphType<CreateReplyCommand>
    {
        public CreateReplyInputGraphType()
        {
            Field(c => c.Comment_Id, type: typeof(IntGraphType));
            Field(c => c.Reply_User_Id, type: typeof(IntGraphType), nullable: true);
            Field(c => c.Text, type: typeof(StringGraphType));
        }
    }
    public class UpdateReplyInputGraphType : InputObjectGraphType<UpdateReplyCommand>
    {
        public UpdateReplyInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
        }
    }
    public class DeleteReplyInputGraphType : InputObjectGraphType<DeleteReplyCommand>
    {
        public DeleteReplyInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
        }
    }
    public class LikeReplyInputGraphType : InputObjectGraphType<LikeReplyCommand>
    {
        public LikeReplyInputGraphType()
        {
            Field(c => c.User_Id, type: typeof(IntGraphType));
            Field(c => c.Reply_Id, type: typeof(IntGraphType));
        }
    }
}
