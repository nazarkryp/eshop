using System;

using MediatR;

namespace eShop.Application.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public CancelOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
