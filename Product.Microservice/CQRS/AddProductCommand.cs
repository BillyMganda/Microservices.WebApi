using MediatR;

namespace Product.Microservice.CQRS
{
    public class AddProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }        
    }
}
