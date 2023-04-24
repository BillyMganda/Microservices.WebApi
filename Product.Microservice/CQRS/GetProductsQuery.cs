using MediatR;
using Product.Microservice.DTOs;

namespace Product.Microservice.CQRS
{
    public class GetProductsQuery : IRequest<List<GetProductDto>>
    {
    }
}
