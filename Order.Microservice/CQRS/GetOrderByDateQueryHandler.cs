using MediatR;
using Order.Microservice.DTOs;
using Order.Microservice.Services;

namespace Order.Microservice.CQRS
{
    public class GetOrderByDateQueryHandler : IRequestHandler<GetOrderByDateQuery, List<GetOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByDateQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderDto>> Handle(GetOrderByDateQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByOrderDateAsync(request.Date);
            if (order == null)
            {
                throw new Exception($"Order with date {request.Date} not found.");
            }

            var orderDtos = order.Select(o => new GetOrderDto
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
