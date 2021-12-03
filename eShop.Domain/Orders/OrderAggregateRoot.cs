using System;

namespace eShop.Domain.Orders
{
    public class OrderAggregateRoot : AggregateRoot
    {
        private Order _order = null!;

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
            return _order;
        }

        public void Start()
        {
            if (_order.State != OrderState.New)
            {
                throw new InvalidOperationException("Operation not valid for order in current state");
            }

            ApplyChanges(new OrderStarted());
        }

        public void Cancel()
        {
            if (_order.State == OrderState.Completed)
            {
                throw new InvalidOperationException("Operation not valid for order in current state");
            }

            ApplyChanges(new OrderCancelled());
        }

        public void Complete()
        {
            if (_order.State != OrderState.InProgress)
            {
                throw new InvalidOperationException("Operation not valid for order in current state");
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
                    State = OrderState.New,
                    LastModified = @event.Date
                };

                Id = _order.OrderId;
            }
            else if (@event.GetType() == typeof(OrderStarted))
            {
                _order.State = OrderState.InProgress;
                _order.LastModified = ((OrderStarted)@event).Date;
            }
            else if (@event.GetType() == typeof(OrderCancelled))
            {
                _order.State = OrderState.Cancelled;
                _order.LastModified = ((OrderCancelled)@event).Date;
            }
            else if (@event.GetType() == typeof(OrderCompleted))
            {
                _order.State = OrderState.Completed;
                _order.LastModified = ((OrderCompleted)@event).Date;
            }
        }
    }
}