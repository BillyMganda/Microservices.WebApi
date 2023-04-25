using MediatR;

namespace Order.Microservice.CQRS
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
    }
}
