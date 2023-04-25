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
            var customers = await _mediator.Send(query);

            return Ok(customers);
        }
    }
}
