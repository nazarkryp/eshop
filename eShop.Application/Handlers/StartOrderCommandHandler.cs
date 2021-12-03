using System.Threading;
using System.Threading.Tasks;

using eShop.Application.Commands;
using eShop.Domain.Orders;
using eShop.Messaging.EventStore;

using MediatR;

namespace eShop.Application.Handlers
{
    internal class StartOrderCommandHandler : IRequestHandler<StartOrderCommand>
    {
        private readonly IEventStore _eventStore;

        public StartOrderCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(StartOrderCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventStreamAsync(request.OrderId);

            var aggregateRoot = new OrderAggregateRoot(events);
            aggregateRoot.Start();

            await _eventStore.AppendStreamAsync(aggregateRoot.Id, aggregateRoot.GetUncommitedEvents());
            aggregateRoot.Commit();

            return Unit.Value;
        }
    }
}
