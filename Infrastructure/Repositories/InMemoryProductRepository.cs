using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public InMemoryProductRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Фиксированные ID для тестов
        var p1 = new Product("Ноутбук Lenovo", "Игровой ноутбук", Money.From(75000), 15, "Ноутбуки");
        var p2 = new Product("iPhone 15", "Смартфон Apple", Money.From(95000), 8, "Смартфоны");
        var p3 = new Product("Монитор Samsung", "27 дюймов 144Hz", Money.From(32000), 12, "Мониторы");
        var p4 = new Product("Клавиатура Logitech", "Механическая", Money.From(6500), 25, "Периферия");

        // Принудительно устанавливаем фиксированные ID
        typeof(Product).GetProperty("Id")!.SetValue(p1, ProductId.From(Guid.Parse("11111111-1111-1111-1111-111111111111")));
        typeof(Product).GetProperty("Id")!.SetValue(p2, ProductId.From(Guid.Parse("22222222-2222-2222-2222-222222222222")));
        typeof(Product).GetProperty("Id")!.SetValue(p3, ProductId.From(Guid.Parse("33333333-3333-3333-3333-333333333333")));
        typeof(Product).GetProperty("Id")!.SetValue(p4, ProductId.From(Guid.Parse("44444444-4444-4444-4444-444444444444")));

        _products.Add(p1);
        _products.Add(p2);
        _products.Add(p3);
        _products.Add(p4);
    }

    public Task<Product?> GetByIdAsync(ProductId id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<List<Product>> GetAllAsync(string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        var query = _products.AsQueryable();

        if (!string.IsNullOrEmpty(category))
            query = query.Where(p => p.Category == category);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price.Amount >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price.Amount <= maxPrice.Value);

        return Task.FromResult(query.ToList());
    }

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }
}