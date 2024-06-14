using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using GraphQL.Types;

namespace Api.GraphQL.Types.CommentTypes
{
    public class CreateCommentInputGraphType : InputObjectGraphType<CreateCommentCommand>
    {
        public CreateCommentInputGraphType()
        {
            Field(c => c.Post_Id, type: typeof(IntGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
        }
    }
    public class GetCommentsInputGraphType : InputObjectGraphType<GetCommentsQuery>
    {
        public GetCommentsInputGraphType()
        {
            Field(p => p.Post_Id, type: typeof(IntGraphType));
            Field(p => p.Next, type: typeof(IntGraphType));
            Field(p => p.Offset, type: typeof(IntGraphType));
            Field(p => p.User_Timestamp, type: typeof(DateTimeGraphType));
            Field(p => p.Order, type: typeof(StringGraphType));
        }
    }
    public class GetCommentByIdInputGraphType : InputObjectGraphType<GetCommentByIdQuery>
    {
        public GetCommentByIdInputGraphType()
        {
            Field(p => p.Id, type: typeof(IntGraphType));
        }
    }
    public class UpdateCommentInputGraphType : InputObjectGraphType<UpdateCommentCommand>
    {
        public UpdateCommentInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
        }
    }
    public class DeleteCommentInputGraphType : InputObjectGraphType<DeleteCommentCommand>
    {
        public DeleteCommentInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
        }
    }
    public class LikeCommentInputGraphType : InputObjectGraphType<LikeCommentCommand>
    {
        public LikeCommentInputGraphType()
        {
            Field(c => c.User_Id, type: typeof(IntGraphType));
            Field(c => c.Comment_Id, type: typeof(IntGraphType));
        }
    }
}
