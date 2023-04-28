using MediatR;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
