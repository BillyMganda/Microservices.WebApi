﻿using MediatR;
using Product.Microservice.Services;
using Product.Microservice.Validations;

namespace Product.Microservice.CQRS
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid == false)
                throw new Exception();

            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new ArgumentException($"Product with Id {request.Id} not found.");
            }
            await _productRepository.DeleteAsync(product);
        }
    }
}
