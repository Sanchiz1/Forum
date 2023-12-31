﻿using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Infrastructure.Data.Repositories;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<DapperContext>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<IReplyRepository, ReplyRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
            services.AddSingleton<ITokenRepository, TokenRepository>();
            services.AddSingleton<ITokenFactory, TokenFactory>();
            services.AddSingleton<ITokenValidator, TokenValidator>();
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddSingleton<IHashingService, HashingService>();

            return services;
        }
    }
}
