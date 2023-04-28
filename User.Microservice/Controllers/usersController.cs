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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand command)
        {
            try
            {
                var userDto = await _mediator.Send(command);
                return Ok(userDto);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginCommand command)
        {
            try
            {
                var Jwt = await _mediator.Send(command);
                return Ok(Jwt);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
