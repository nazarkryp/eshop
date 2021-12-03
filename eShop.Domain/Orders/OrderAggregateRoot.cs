using System;

namespace eShop.Domain.Orders
{
    public class OrderAggregateRoot : AggregateRoot
    {
        private Order? _order;

        public OrderAggregateRoot()
        {
            Id = Guid.NewGuid();

            _order = new Order();

            ApplyChanges(new OrderCreated
            {
                OrderId = Id
            });
        }

        public OrderAggregateRoot(Event[] events)
        {
            foreach (var @event in events)
            {
                ApplyChanges(@event);
            }
        }

        public Order GetOrder()
        {
            if (_order == null)
            {
                throw new ArgumentNullException(nameof(_order), "Order not created");
            }

            return _order;
        }

        public void Start()
        {
            if (_order == null || _order.State != OrderState.New)
            {
                throw new InvalidOperationException();
            }

            ApplyChanges(new OrderStarted());
        }

        public void Complete()
        {
            if (_order == null || _order.State != OrderState.InProgress)
            {
                throw new InvalidOperationException();
            }

            ApplyChanges(new OrderCompleted());
        }

        protected override void Apply(Event @event)
        {
            if (@event.GetType() == typeof(OrderCreated))
            {
                _order = new Order
                {
                    OrderId = ((OrderCreated)@event).OrderId,
                    State = OrderState.New
                };

                Id = _order.OrderId;
            }
            else if (@event.GetType() == typeof(OrderStarted))
            {
                _order.State = OrderState.InProgress;
            }
            else if (@event.GetType() == typeof(OrderCompleted))
            {
                _order.State = OrderState.Completed;
            }
        }
    }
}