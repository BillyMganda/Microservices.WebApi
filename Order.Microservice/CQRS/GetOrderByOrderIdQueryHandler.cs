using MediatR;
using Order.Microservice.DTOs;
using Order.Microservice.Exceptions;
using Order.Microservice.Services;

namespace Order.Microservice.CQRS
{
    public class GetOrderByOrderIdQueryHandler : IRequestHandler<GetOrderByOrderIdQuery, GetOrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByOrderIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderDto> Handle(GetOrderByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByOrderIdAsync(request.OrderId);
            if (order == null)
            {
                throw new NotFoundException(nameof(GetOrderByDateQuery), request.OrderId);
            }

            var result = new GetOrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductIds,
                OrderDate = order.OrderDate,
                Price = order.Price,
                Quantity = order.Quantity,                
            };

            return result;
        }
    }
}
