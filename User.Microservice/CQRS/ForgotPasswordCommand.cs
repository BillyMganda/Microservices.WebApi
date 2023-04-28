using MediatR;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
    }
}
