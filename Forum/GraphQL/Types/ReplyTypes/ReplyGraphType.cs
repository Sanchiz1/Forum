﻿using Forum.Models.Posts;
using Forum.Models.Replies;
using GraphQL.Types;

namespace Forum.GraphQL.Types.ReplyTypes
{
    public class ReplyGraphType : ObjectGraphType<Reply>
    {
        public ReplyGraphType()
        {
            Field(i => i.Id, type: typeof(IntGraphType));
            Field(i => i.Text, type: typeof(StringGraphType));
            Field(i => i.Date, type: typeof(DateTimeGraphType));
            Field(i => i.Comment_Id, type: typeof(IntGraphType));
            Field(i => i.Reply_Id, type: typeof(IntGraphType));
            Field(i => i.User_Id, type: typeof(IntGraphType), nullable: true);
            Field(i => i.User_Username, type: typeof(StringGraphType));
            Field(i => i.Reply_Username, type: typeof(StringGraphType), nullable: true);
        }
    }
}
