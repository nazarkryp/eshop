using System;

using MediatR;

namespace eShop.Application.Commands
{
    public class CompleteOrderCommand : IRequest
    {
        public CompleteOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get;  }
    }
}
