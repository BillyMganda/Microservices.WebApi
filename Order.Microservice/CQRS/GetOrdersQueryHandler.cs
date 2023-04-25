using MediatR;
using Order.Microservice.DTOs;
using Order.Microservice.Services;

namespace Order.Microservice.CQRS
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<GetOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }             

        async Task<List<GetOrderDto>> IRequestHandler<GetOrdersQuery, List<GetOrderDto>>.Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = orders.Select(o => new GetOrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                ProductId = o.ProductIds,
                OrderDate = o.OrderDate,
                Price = o.Price,
                Quantity = o.Quantity
            }).ToList();

            return orderDtos;
        }
    }
}
