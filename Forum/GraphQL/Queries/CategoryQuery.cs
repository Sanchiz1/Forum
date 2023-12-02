using Application.UseCases.Categories.Queries;
using Forum.GraphQL.Types.CategoryTypes;
using Forum.GraphQL.Types.CommentTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Queries
{
    public class CategoryQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public CategoryQuery(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;

            Field<ListGraphType<CategoryGraphType>>("categories")
                .Argument<NonNullGraphType<GetCategoriesInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCategoriesQuery getCategoriesQuery = new GetCategoriesQuery()
                    {
                        Next = context.GetArgument<GetCategoriesQuery>("input").Next,
                        Offset = context.GetArgument<GetCategoriesQuery>("input").Offset,
                    };
                    return await _mediator.Send(getCategoriesQuery);
                });
            Field<CategoryGraphType>("category")
                .Argument<NonNullGraphType<GetCategoryByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCategoryByIdQuery getCategoryByIdQuery = new GetCategoryByIdQuery()
                    {
                        Id = context.GetArgument<GetCategoryByIdQuery>("input").Id
                    };

                    try
                    {
                        return await _mediator.Send(getCategoryByIdQuery);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Logging in");
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                });
        }
    }
}
