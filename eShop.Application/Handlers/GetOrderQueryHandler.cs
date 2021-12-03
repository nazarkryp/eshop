using System.Threading;
using System.Threading.Tasks;

using eShop.Application.Queries;
using eShop.Domain.Orders;
using eShop.Messaging.EventStore;

using MediatR;

namespace eShop.Application.Handlers
{
    internal class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IEventStore _eventStore;

        public GetOrderQueryHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventStreamAsync(request.OrderId);

            var aggregateRoot = new OrderAggregateRoot(events);

            return aggregateRoot.GetOrder();
        }
    }
}
