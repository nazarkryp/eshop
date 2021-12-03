using System;

namespace eShop.Domain.Orders
{
    internal class OrderCancelled : Event
    {
        public Guid OrderId { get; set; }
    }
}
