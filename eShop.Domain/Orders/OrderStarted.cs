﻿using System;

namespace eShop.Domain.Orders
{
    internal class OrderStarted : Event
    {
        public Guid OrderId { get; set; }
    }
}
