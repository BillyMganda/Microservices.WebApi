using Customer.Microservice.CQRS;
using Customer.Microservice.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public customersController(IMediator mediator)
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

        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] AddCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);

            return CreatedAtRoute("GetCustomerById", new { id = customerId }, new { CustomerId = customerId });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {  
            var result = await _mediator.Send(command);

            if (result == "Customer updated successfully.")
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteCustomerCommand { Id = id });
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Customer not found");
            }
        }

    }
}
