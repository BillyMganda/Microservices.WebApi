using MediatR;
using Order.Microservice.DTOs;

namespace Order.Microservice.CQRS
{
    public class GetOrderByDateQuery : IRequest<List<GetOrderDto>>
    {
        public DateTime Date { get; set; }
    }
}
