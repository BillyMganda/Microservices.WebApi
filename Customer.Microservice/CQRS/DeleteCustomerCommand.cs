using MediatR;

namespace Customer.Microservice.CQRS
{
    public class DeleteCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
