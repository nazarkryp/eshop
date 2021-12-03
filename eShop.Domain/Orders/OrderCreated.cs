using System;

namespace eShop.Domain.Orders
{
    public class OrderCreated : Event
    {
        public Guid OrderId { get; set; }
    }
}
