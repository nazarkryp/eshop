using eShop.Application.Commands;
using eShop.Application.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0"), Route("v{version:apiVersion}/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ISender sender, ILogger<OrdersController> logger)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrderAsync()
        {
            var orderId = await _sender.Send(new CreateOrderCommand
            {
                ShoppingCartId = Guid.NewGuid()
            });

            return Ok(new
            {
                OrderId = orderId
            });
        }

        [HttpGet(Name = "GetOrder")]
        public async Task<IActionResult> GetOrderAsync(Guid orderId)
        {
            var order = await _sender.Send(new GetOrderQuery(orderId));

            return Ok(order);
        }

        [HttpPost("{orderId}/start", Name = "StartOrder")]
        public async Task<IActionResult> StartOrderAsync(Guid orderId)
        {
            await _sender.Send(new StartOrderCommand(orderId));

            return Ok();
        }

        [HttpPost("{orderId}/complete", Name = "Complete")]
        public async Task<IActionResult> CompleteOrderAsync(Guid orderId)
        {
            await _sender.Send(new CompleteOrderCommand(orderId));

            return Ok();
        }
    }
}