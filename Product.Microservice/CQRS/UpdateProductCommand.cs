using MediatR;

namespace Product.Microservice.CQRS
{
    public class UpdateProductCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
    }
}
