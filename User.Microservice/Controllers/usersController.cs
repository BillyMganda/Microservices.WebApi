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
            var userDto = await _mediator.Send(command);
            return Ok(userDto);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> UserLogin([FromBody] LoginCommand command)
        {
            var Jwt = await _mediator.Send(command);
            return Ok(Jwt);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> GenerateRefreshToken()
        {
            // TODO
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            // TODO
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            // TODO
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok("Request successful, check your email for password reset token");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok("Operation successful");
        }
    }
}
