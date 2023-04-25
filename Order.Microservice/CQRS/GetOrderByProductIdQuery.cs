using MediatR;
using Order.Microservice.DTOs;

namespace Order.Microservice.CQRS
{
    public class GetOrderByProductIdQuery : IRequest<List<GetOrderDto>>
    {
        public Guid ProductId { get; set; }
    }
}
