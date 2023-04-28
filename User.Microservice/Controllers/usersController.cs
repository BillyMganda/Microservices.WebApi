using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Microservice.CQRS;

namespace User.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public usersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand command)
        {
            var userDto = await _mediator.Send(command);
            return Ok(userDto);
        }
    }
}
