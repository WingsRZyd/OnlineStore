using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.UseCases;

public class GetProductsUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductsUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> ExecuteAsync(string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        return await _productRepository.GetAllAsync(category, minPrice, maxPrice);
    }
}