using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Microservice.CQRS;
using Order.Microservice.DTOs;

namespace Order.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ordersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ordersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderDto>>> GetAll()
        {
            var query = new GetOrdersQuery();
            var orders = await _mediator.Send(query);

            return Ok(orders);
        }

        [HttpGet("{orderid}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderByOrderId(Guid OrderId)
        {
            var query = new GetOrderByOrderIdQuery { OrderId = OrderId };
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("{customerid}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderByCustomerId(Guid CustomerId)
        {
            var query = new GetOrderByCustomerIdQuery { CustomerId = CustomerId };
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
