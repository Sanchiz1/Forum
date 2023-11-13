using Application;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Queries;
using Infrastructure;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {

        //async Task Main feature allowed from C# 7.1+
        public static async Task Main(string[] args)
        {
            await RunAsync(ConfigureServices());
        }

        private static async Task RunAsync(IServiceProvider services)
        {
            var mediatr = services.GetRequiredService<IMediator>();
            var result = mediatr.Send(new GetUserByIdQuery { User_Id = 1 });
            Console.WriteLine(result.Result.Username);
        }




        public static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            services.AddSingleton<IConfigurationRoot>(configuration);
            services.AddSingleton<IConfiguration>(configuration);

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);

            return services.BuildServiceProvider();
        }
        public class ApplicationLogs
        {
        }
    }
}
