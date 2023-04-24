using MediatR;
using Product.Microservice.DTOs;

namespace Product.Microservice.CQRS
{
    public class GetProductByIdQuery : IRequest<GetProductDto>
    {
        public Guid Id { get; set; }
    }
}
