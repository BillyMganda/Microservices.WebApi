using MediatR;
using Order.Microservice.DTOs;

namespace Order.Microservice.CQRS
{
    public class GetOrderByOrderIdQuery : IRequest<GetOrderDto>
    {
        public Guid OrderId { get; set; }
    }
}
