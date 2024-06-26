﻿using Application.UseCases.Statistics.Queries;
using GraphQL.Types;

namespace Api.GraphQL.Types.StatisticsTypes
{
    public class GetMonthlyPostsInputGraphType : InputObjectGraphType<GetMonthlyPostsQuery>
    {
        public GetMonthlyPostsInputGraphType()
        {
            Field(p => p.Year, type: typeof(IntGraphType));
        }
    }
    public class GetMonthlyUsersInputGraphType : InputObjectGraphType<GetMonthlyUsersQuery>
    {
        public GetMonthlyUsersInputGraphType()
        {
            Field(p => p.Year, type: typeof(IntGraphType));
        }
    }
}
