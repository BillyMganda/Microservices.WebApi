using MediatR;

namespace Product.Microservice.CQRS
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
