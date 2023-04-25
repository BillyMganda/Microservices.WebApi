using MediatR;

namespace Order.Microservice.CQRS
{
    public class PostOrderCommand : IRequest<Guid>
    {
        public List<Guid> ProductIds { get; set; } = new List<Guid>();        
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
