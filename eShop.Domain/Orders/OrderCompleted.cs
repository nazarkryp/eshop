using System;

namespace eShop.Domain.Orders
{
    internal class OrderCompleted : Event
    {
        public Guid OrderId { get; set; }
    }
}
