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

        [HttpGet]
        [Route("get-by-orderid/{orderid}")]
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

        [HttpGet]
        [Route("get-by-customerid/{customerid}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderByCustomerId(Guid customerId)
        {
            var query = new GetOrderByCustomerIdQuery { CustomerId = customerId };
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        [Route("get-by-productid/{productid}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderByProductId(Guid productId)
        {
            var query = new GetOrderByProductIdQuery { ProductId = productId };
            var order = await _mediator.Send(query);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("{date}")]
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

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                await _mediator.Send(new DeleteOrderCommand { OrderId = orderId });
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
