using System;
using System.Threading.Tasks;

using eShop.Domain;

namespace eShop.Messaging.EventStore
{
    public interface IEventStore
    {
        Task AppendStreamAsync(Guid streamId, Event[] events);

        Task<Event[]> GetEventStreamAsync(Guid streamId);
    }
}
