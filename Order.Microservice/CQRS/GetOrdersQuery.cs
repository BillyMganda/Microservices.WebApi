using MediatR;
using Order.Microservice.DTOs;

namespace Order.Microservice.CQRS
{
    public class GetOrdersQuery : IRequest<GetOrderDto>
    {
    }
}
