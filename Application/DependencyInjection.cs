using Application.Common.Behaviors;
using Application.Common.DTOs;
using Application.UseCases.Posts.Commands;
using AutoMapper;
using FluentValidation;
using FluentValidation.DependencyInjectionExtensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service.ServiceType.FullName} \nLifetime: { service.Lifetime} \nInstance: { service.ImplementationType?.FullName} ");
            }
            return services;
        }
    }
}
