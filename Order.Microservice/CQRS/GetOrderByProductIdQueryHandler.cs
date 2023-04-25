using MediatR;
using Order.Microservice.DTOs;
using Order.Microservice.Services;

namespace Order.Microservice.CQRS
{
    public class GetOrderByProductIdQueryHandler : IRequestHandler<GetOrderByProductIdQuery, List<GetOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByProductIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderDto>> Handle(GetOrderByProductIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByProductIdAsync(request.ProductId);

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
