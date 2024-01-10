using Application.UseCases.Statistics.Queries;
using Forum.GraphQL.Types.StatisticsTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;

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
                    GetMonthlyPostsQuery query = context.GetArgument<GetMonthlyPostsQuery>("input");

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<ListGraphType<IntGraphType>>("getMonthlyUsers")
                .Argument<NonNullGraphType<GetMonthlyUsersInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetMonthlyUsersQuery query = context.GetArgument<GetMonthlyUsersQuery>("input");

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
