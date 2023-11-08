﻿using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using GraphQL.Types;
using GraphQL;
using Forum.GraphQL.Types.CommentTypes;

namespace Forum.GraphQL.Types.ReplyTypes
{
    public class ReplyQuery : ObjectGraphType
    {
        private readonly IReplyRepository repo;
        public ReplyQuery(IReplyRepository Repo)
        {
            repo = Repo;

            Field<ListGraphType<ReplyGraphType>>("replies")
                .Argument<NonNullGraphType<IntGraphType>>("comment_id")
                .Argument<NonNullGraphType<IntGraphType>>("offset")
                .Argument<NonNullGraphType<IntGraphType>>("next")
                .Argument<NonNullGraphType<StringGraphType>>("order")
                .Argument<NonNullGraphType<DateTimeGraphType>>("user_timestamp")
                .Resolve(context =>
                {
                    int comment_id = context.GetArgument<int>("comment_id");
                    int offset = context.GetArgument<int>("offset");
                    int next = context.GetArgument<int>("next");
                    string order = context.GetArgument<string>("order");
                    DateTime user_timestamp = context.GetArgument<DateTime>("user_timestamp");
                    return repo.GetReplies(comment_id, next, offset, user_timestamp, order);
                });
        }
    }
}
