using eShop.Domain;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(builder =>
        {
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        })
        .ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;

            services.AddLogging();

            //services.AddSingleton<IMongoConfiguration, MongoConfiguration>(serviceProvider =>
            //{
            //    var section = configuration.GetSection("MongoDb");
            //    var connectionString = section.GetValue<string>("ConnectionString");
            //    var database = section.GetValue<string>("Database");

            //    return new MongoConfiguration(connectionString, database);
            //});
        });
}