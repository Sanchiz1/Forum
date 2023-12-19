using Application.UseCases.Statistics.Queries;
using Application.UseCases.Users.Queries;
using Forum.GraphQL.Types.StatisticsTypes;
using Forum.GraphQL.Types.UserTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forum.GraphQL.Queries
{
    public class StatisticsQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public StatisticsQuery(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;

            Field<ListGraphType<IntGraphType>>("getMonthlyPosts")
                .Argument<NonNullGraphType<GetMonthlyPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetMonthlyPostsQuery getMonthlyPostsInputGraphType = context.GetArgument<GetMonthlyPostsQuery>("input");
                    try
                    {
                    return await _mediator.Send(getMonthlyPostsInputGraphType);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Getting searched posts");
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                });

            Field<ListGraphType<IntGraphType>>("getMonthlyUsers")
                .Argument<NonNullGraphType<GetMonthlyUsersInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetMonthlyUsersQuery getMonthlyUsersQuery = context.GetArgument<GetMonthlyUsersQuery>("input");
                    try
                    {
                        return await _mediator.Send(getMonthlyUsersQuery);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Getting searched posts");
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                });
        }
    }
}
