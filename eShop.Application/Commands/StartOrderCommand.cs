using System;

using MediatR;

namespace eShop.Application.Commands
{
    public class StartOrderCommand : IRequest
    {
        public StartOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
