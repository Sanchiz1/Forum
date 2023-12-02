using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CategoryTypes
{
    public class CreateCategoryInputGraphType : InputObjectGraphType<CreateCategoryCommand>
    {
        public CreateCategoryInputGraphType()
        {
            Field(c => c.Title, type: typeof(StringGraphType));
        }
    }
    public class GetCategoriesInputGraphType : InputObjectGraphType<GetCategoriesQuery>
    {
        public GetCategoriesInputGraphType()
        {
            Field(p => p.Next, type: typeof(IntGraphType));
            Field(p => p.Offset, type: typeof(IntGraphType));
        }
    }
    public class GetCategoryByIdInputGraphType : InputObjectGraphType<GetCategoryByIdQuery>
    {
        public GetCategoryByIdInputGraphType()
        {
            Field(p => p.Id, type: typeof(IntGraphType));
        }
    }
    public class UpdateCategoryInputGraphType : InputObjectGraphType<UpdateCategoryCommand>
    {
        public UpdateCategoryInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
            Field(c => c.Title, type: typeof(StringGraphType));
        }
    }
    public class DeleteCategoryInputGraphType : InputObjectGraphType<DeleteCategoryCommand>
    {
        public DeleteCategoryInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
        }
    }
}
