using Application.UseCases.Categories.Queries;
using Forum.GraphQL.Types.CategoryTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Queries
{
    public class CategoryQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        public CategoryQuery(IMediator mediator)
        {
            _mediator = mediator;

            Field<ListGraphType<CategoryGraphType>>("categories")
                .Argument<NonNullGraphType<GetCategoriesInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCategoriesQuery query = context.GetArgument<GetCategoriesQuery>("input");

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<ListGraphType<CategoryGraphType>>("allCategories")
                .ResolveAsync(async context =>
                {
                    GetAllCategoriesQuery query = new GetAllCategoriesQuery();

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<CategoryGraphType>("category")
                .Argument<NonNullGraphType<GetCategoryByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCategoryByIdQuery query = context.GetArgument<GetCategoryByIdQuery>("input");

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
