using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Microservice.CQRS;
using Product.Microservice.DTOs;

namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public productsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAll()
        {
            var query = new GetProductsQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var Product = await _mediator.Send(query);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddProduct([FromBody] AddProductCommand command)
        {
            var ProductId = await _mediator.Send(command);

            return Ok(ProductId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == "Product updated successfully.")
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
