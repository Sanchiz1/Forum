using Application.UseCases.Categories.Commands;
using Application.UseCases.Comments.Commands;
using FluentValidation;
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
                    try
                    {
                        await _mediator.Send(createCategoryCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                    return "Category created successfully";
                });

            Field<StringGraphType>("updateCategory")
                .Argument<NonNullGraphType<UpdateCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateCategoryCommand updateCategoryCommand = context.GetArgument<UpdateCategoryCommand>("input");
                    try
                    {
                        await _mediator.Send(updateCategoryCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                    return "Category updated successfully";
                });

            Field<StringGraphType>("deleteCategory")
                .Argument<NonNullGraphType<DeleteCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteCategoryCommand deleteCategoryCommand = context.GetArgument<DeleteCategoryCommand>("input");
                    try
                    {
                        await _mediator.Send(deleteCategoryCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                    return "Category deleted successfully";
                });
        }
        }
}
