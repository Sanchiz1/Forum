using Application.Common.Exceptions;
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
                    CreateCategoryCommand createCategoryCommand = new CreateCategoryCommand()
                    {
                        Title = context.GetArgument<CreateCategoryCommand>("input").Title,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    try
                    {
                        await _mediator.Send(createCategoryCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
                    UpdateCategoryCommand updateCategoryCommand = new UpdateCategoryCommand()
                    {
                        Id = context.GetArgument<UpdateCategoryCommand>("input").Id,
                        Title = context.GetArgument<UpdateCategoryCommand>("input").Title,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    try
                    {
                        await _mediator.Send(updateCategoryCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
                    DeleteCategoryCommand deleteCategoryCommand = new DeleteCategoryCommand()
                    {
                        Id = context.GetArgument<DeleteCategoryCommand>("input").Id,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    try
                    {
                        await _mediator.Send(deleteCategoryCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
