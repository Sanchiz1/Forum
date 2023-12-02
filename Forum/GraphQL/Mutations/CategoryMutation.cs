using Application.UseCases.Categories.Commands;
using Forum.GraphQL.Types.CategoryTypes;
using Forum.GraphQL.Types.CommentTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Mutations
{
    public class CategoryMutation : ObjectGraphType
    {
        private readonly IMediator _mediator;

        public CategoryMutation(IMediator mediator)
        {
            _mediator = mediator;

            Field<StringGraphType>("createCategory")
                .Argument<NonNullGraphType<CreateCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    CreateCategoryCommand createCategoryCommand = context.GetArgument<CreateCategoryCommand>("input");

                    await _mediator.Send(createCategoryCommand);
                    return "Category created successfully";
                });

            Field<StringGraphType>("updateCategory")
                .Argument<NonNullGraphType<UpdateCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateCategoryCommand updateCategoryCommand = context.GetArgument<UpdateCategoryCommand>("input");

                    await _mediator.Send(updateCategoryCommand);
                    return "Category updated successfully";
                });

            Field<StringGraphType>("deleteCategory")
                .Argument<NonNullGraphType<DeleteCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteCategoryCommand deleteCategoryCommand = context.GetArgument<DeleteCategoryCommand>("input");

                    await _mediator.Send(deleteCategoryCommand);
                    return "Category deleted successfully";
                });
        }
        }
}
