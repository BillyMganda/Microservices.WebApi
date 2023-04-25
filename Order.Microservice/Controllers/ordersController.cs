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

        [HttpGet("{productid}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderByProductId(Guid ProductId)
        {
            var query = new GetOrderByProductIdQuery { ProductId = ProductId };
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("{orderdate}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderByOrderDate(DateTime date)
        {
            var query = new GetOrderByDateQuery { Date = date };
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddOrder([FromBody] PostOrderCommand command)
        {
            var OrderId = await _mediator.Send(command);

            return Ok(OrderId);
        }

        [HttpDelete("{orderid}")]
        public async Task<IActionResult> DeleteOrder(Guid orderid)
        {
            try
            {
                await _mediator.Send(new DeleteOrderCommand { OrderId = orderid });
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
