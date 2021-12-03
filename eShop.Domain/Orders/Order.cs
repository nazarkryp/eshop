using System;

namespace eShop.Domain.Orders
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public OrderState State { get; set; }

        public DateTime LastModified { get; set; }
    }
}
