using MediatR;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommand : IRequest<string>
    {
    }
}
