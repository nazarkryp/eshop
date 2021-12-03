using System;
using System.Collections.Generic;
using System.Linq;

namespace eShop.Domain
{
    public class Event
    {
        public Guid? EventId { get; set; }
    }

    public abstract class AggregateRoot
    {
        private readonly IList<Event> _events = new List<Event>();

        public Guid Id { get; protected set; }

        public Event[] GetUncommitedEvents()
        {
            return _events.Where(e => e.EventId == null).ToArray();
        }

        public void Commit()
        {
            _events.Clear();
        }

        protected void ApplyChanges(Event @event)
        {
            _events.Add(@event);
            Apply(@event);
        }

        protected abstract void Apply(Event @event);
    }
}
