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
                    CreateCategoryCommand command = new CreateCategoryCommand()
                    {
                        Title = context.GetArgument<CreateCategoryCommand>("input").Title,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateCategory")
                .Argument<NonNullGraphType<UpdateCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateCategoryCommand command = new UpdateCategoryCommand()
                    {
                        Id = context.GetArgument<UpdateCategoryCommand>("input").Id,
                        Title = context.GetArgument<UpdateCategoryCommand>("input").Title,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("deleteCategory")
                .Argument<NonNullGraphType<DeleteCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteCategoryCommand command = new DeleteCategoryCommand()
                    {
                        Id = context.GetArgument<DeleteCategoryCommand>("input").Id,
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
        }
}
