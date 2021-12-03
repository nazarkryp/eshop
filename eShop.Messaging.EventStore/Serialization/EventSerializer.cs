using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using eShop.Domain;

using EventStore.Client;

namespace eShop.Messaging.EventStore.Serialization
{
    internal interface IEventSerializer
    {
        Event Deserialize(ResolvedEvent resolvedEvent);
    }

    internal class EventSerializer : IEventSerializer
    {
        private readonly Dictionary<string, Type> _eventTypes;

        public EventSerializer(Dictionary<string, Type>? eventTypes)
        {
            _eventTypes = eventTypes ?? throw new ArgumentNullException(nameof(eventTypes), "Event types missing.");
        }

        public Event Deserialize(ResolvedEvent resolvedEvent)
        {
            if (!_eventTypes.TryGetValue(resolvedEvent.Event.EventType, out var type))
            {
                throw new ArgumentNullException(nameof(type), $"Could not resolve type '{resolvedEvent.Event.EventType}'");
            }

            var json = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
            var @event = JsonSerializer.Deserialize(json, type) as Event;

            if (@event == null)
            {
                throw new ArgumentNullException($"Could not resolve type '{resolvedEvent.Event.EventType}'");
            }

            @event.EventId = resolvedEvent.Event.EventId.ToGuid();

            return @event;
        }
    }
}
