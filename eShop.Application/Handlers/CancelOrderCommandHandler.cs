using System.Threading;
using System.Threading.Tasks;

using eShop.Application.Commands;
using eShop.Domain.Orders;
using eShop.Messaging.EventStore;

using MediatR;

namespace eShop.Application.Handlers
{
    internal class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IEventStore _eventStore;

        public CancelOrderCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventStreamAsync(request.OrderId);

            var aggregateRoot = new OrderAggregateRoot(events);
            aggregateRoot.Cancel();

            await _eventStore.AppendStreamAsync(aggregateRoot.Id, aggregateRoot.GetUncommitedEvents());
            aggregateRoot.Commit();

            return Unit.Value;
        }
    }
}
