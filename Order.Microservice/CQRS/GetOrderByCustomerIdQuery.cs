using MediatR;
using Order.Microservice.DTOs;

namespace Order.Microservice.CQRS
{
    public class GetOrderByCustomerIdQuery : IRequest<GetOrderDto>
    {
        public Guid CustomerId { get; set; }
    }
}
