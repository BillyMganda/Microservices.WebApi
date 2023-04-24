using MediatR;
using Product.Microservice.DTOs;
using Product.Microservice.Services;

namespace Product.Microservice.CQRS
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductDto>>
    {
        private readonly IProductRepository _repository;
        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            var results = products.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                LastModifiedDate = p.LastModifiedDate,
            }
            ).ToList();

            return results;
        }
    }
}
