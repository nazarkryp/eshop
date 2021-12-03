using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using eShop.Domain;
using eShop.Messaging.EventStore.Serialization;

using EventStore.Client;

namespace eShop.Messaging.EventStore
{
    internal class OrdersEventStore : IEventStore
    {
        private const string OrdersEventStream = "orders";

        private readonly EventStoreClient _eventStoreClient;
        private readonly IEventSerializer _eventSerializer;

        public OrdersEventStore(EventStoreClient eventStoreClient, IEventSerializer eventSerializer)
        {
            _eventStoreClient = eventStoreClient;
            _eventSerializer = eventSerializer;
        }

        public async Task AppendStreamAsync(Guid streamId, Event[] events)
        {
            var eventsData = events.Select(@event => new EventData(Uuid.NewUuid(), @event.GetType().Name, JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType())));

            var writeResult = await _eventStoreClient.AppendToStreamAsync(GetStreamName(streamId), StreamState.Any, eventsData);
        }

        public async Task<Event[]> GetEventStreamAsync(Guid streamId)
        {
            var result = _eventStoreClient.ReadStreamAsync(Direction.Forwards, GetStreamName(streamId), StreamPosition.Start);

            var events = await result.ToListAsync();

            return events.Select(_eventSerializer.Deserialize).ToArray();
        }

        private static string GetStreamName(Guid streamId)
            => $"{OrdersEventStream}-{streamId}";
    }
}
