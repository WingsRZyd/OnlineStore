using OnlineStore.Domain.Entities;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id);
    Task<List<Product>> GetAllAsync(string? category = null, decimal? minPrice = null, decimal? maxPrice = null);
    Task AddAsync(Product product);
}