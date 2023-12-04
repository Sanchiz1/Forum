using Application.UseCases.Posts.Queries;
using Domain.Entities;
using Forum.GraphQL.Types.PostTypes;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Forum.GraphQL.Queries
{
    public class PostQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public PostQuery(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;

            Field<ListGraphType<PostGraphType>>("posts")
                .Argument<NonNullGraphType<GetPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    try
                    {
                        GetPostsQuery getPostsQuery = new GetPostsQuery()
                        {
                            Next = context.GetArgument<GetPostsQuery>("input").Next,
                            Offset = context.GetArgument<GetPostsQuery>("input").Offset,
                            Order = context.GetArgument<GetPostsQuery>("input").Order,
                            User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                            User_Timestamp = context.GetArgument<GetPostsQuery>("input").User_Timestamp,
                            Categories = context.GetArgument<GetPostsQuery>("input").Categories,
                        };
                        return await _mediator.Send(getPostsQuery);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Getting posts");
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                });

            Field<ListGraphType<PostGraphType>>("userPosts")
                .Argument<NonNullGraphType<GetUserPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    try
                    {
                        GetUserPostsQuery getUserPostsQuery = new GetUserPostsQuery()
                        {
                            Author_Username = context.GetArgument<GetUserPostsQuery>("input").Author_Username,
                            Next = context.GetArgument<GetUserPostsQuery>("input").Next,
                            Offset = context.GetArgument<GetUserPostsQuery>("input").Offset,
                            Order = context.GetArgument<GetUserPostsQuery>("input").Order,
                            User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                            User_Timestamp = context.GetArgument<GetUserPostsQuery>("input").User_Timestamp,
                        };
                        return await _mediator.Send(getUserPostsQuery);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Getting user posts");
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                });

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<GetPostByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostByIdQuery getPostByIdQuery = new GetPostByIdQuery()
                    {
                        Id = context.GetArgument<GetPostByIdQuery>("input").Id,
                        User_id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getPostByIdQuery);
                });
        }
    }
}
