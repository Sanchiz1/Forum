using Application.Common.DTOs;
using GraphQL.Types;

namespace Api.GraphQL.Types.CategoryTypes
{
    public class CategoryGraphType : ObjectGraphType<CategoryDto>
    {
        public CategoryGraphType()
        {
            Field(i => i.Id, type: typeof(IntGraphType));
            Field(i => i.Title, type: typeof(StringGraphType));
        }
    }
}
