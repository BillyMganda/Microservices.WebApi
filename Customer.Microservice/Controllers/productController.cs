using Customer.Microservice.CQRS;
using Customer.Microservice.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly IMediator _mediator;
        public productController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCustomerDto>>> GetAll()
        {
            var query = new GetCustomersQuery();
            var customers = await _mediator.Send(query);

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> GetCustomerById(Guid id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var customer = await _mediator.Send(query);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
