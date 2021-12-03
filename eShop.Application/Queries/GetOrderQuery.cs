using System;

using eShop.Domain.Orders;

using MediatR;

namespace eShop.Application.Queries
{
    public class GetOrderQuery : IRequest<Order>
    {
        public GetOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
