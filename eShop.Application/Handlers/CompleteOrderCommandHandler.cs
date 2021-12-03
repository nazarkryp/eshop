using System.Threading;
using System.Threading.Tasks;

using eShop.Application.Commands;
using eShop.Domain.Orders;
using eShop.Messaging.EventStore;

using MediatR;

namespace eShop.Application.Handlers
{
    internal class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
    {
        private readonly IEventStore _eventStore;

        public CompleteOrderCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var events  = await _eventStore.GetEventStreamAsync(request.OrderId);

            var aggregateRoot = new OrderAggregateRoot(events);
            aggregateRoot.Complete();

            await _eventStore.AppendStreamAsync(aggregateRoot.Id, aggregateRoot.GetUncommitedEvents());

            return Unit.Value;
        }
    }
}
