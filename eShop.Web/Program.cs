using System.Text.Json;
using System.Text.Json.Serialization;

using eShop.Application;
using eShop.Messaging.EventStore;
using eShop.Web.Infrastructure.Errors;
using eShop.Web.Infrastructure.Filters;
using eShop.Web.Infrastructure.Swagger;

using EventStore.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
    .SetMinimumLevel(LogLevel.Information);

builder.Services
    .AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = ApiVersion.Default;
    })
    .AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddSwagger();

builder.Services.AddSingleton<WebApiErrorProvider>();
builder.Services.AddMessaging();
builder.Services.AddEventStore();

builder.Services.AddScoped<EventStoreClient>(_ =>
{
    var connectionString = builder.Configuration.GetSection("EventStore").GetValue<string>("ConnectionString");
    var settings = EventStoreClientSettings.Create(connectionString);

    return new EventStoreClient(settings);
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<DefaultExceptionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwagger(apiVersionDescriptionProvider);
app.MapControllers();
app.Run();
