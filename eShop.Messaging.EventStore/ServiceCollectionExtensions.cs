using System.Linq;
using System.Reflection;

using eShop.Domain;
using eShop.Messaging.EventStore.Serialization;

using Microsoft.Extensions.DependencyInjection;

namespace eShop.Messaging.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services)
        {
            services.AddScoped<IEventStore, OrdersEventStore>();

            services.AddSingleton<IEventSerializer, EventSerializer>(_ =>
            {
                var eventTypes = Assembly.GetAssembly(typeof(Event))?.GetTypes()
                    .Where(e => e != typeof(Event) && typeof(Event).IsAssignableFrom(e))
                    .ToDictionary(e => e.Name, e => e);

                return new EventSerializer(eventTypes);
            });

            return services;
        }
    }
}
