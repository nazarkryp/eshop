using System;
using System.Threading;
using System.Threading.Tasks;

using eShop.Application.Commands;
using eShop.Domain.Orders;
using eShop.Messaging.EventStore;

using MediatR;

namespace eShop.Application.Handlers
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IEventStore _eventStore;

        public CreateOrderCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderAggregateRoot = new OrderAggregateRoot();

            await _eventStore.AppendStreamAsync(orderAggregateRoot.Id, orderAggregateRoot.GetUncommitedEvents());

            return orderAggregateRoot.Id;
        }
    }
}
