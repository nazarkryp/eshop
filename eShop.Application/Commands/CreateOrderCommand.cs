using System;

using MediatR;

namespace eShop.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid ShoppingCartId { get; set; }
    }
}
