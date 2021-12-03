using eShop.Messaging.EventStore;

namespace eShop.Web.Infrastructure.Services
{
    public class EventsHostedService : IHostedService
    {
        private readonly IEventStore _eventStore;
        private readonly ILogger<EventsHostedService> _logger;

        public EventsHostedService(IEventStore eventStore, ILogger<EventsHostedService> logger)
        {
            _eventStore = eventStore;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
