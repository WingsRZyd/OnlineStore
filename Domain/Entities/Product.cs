using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Domain.Entities;

public class Product
{
    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public int Stock { get; private set; }
    public string Category { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Product() { }

    public Product(string name, string description, Money price, int stock, string category)
    {
        Id = ProductId.New();
        Name = name.Trim();
        Description = description.Trim();
        Price = price;
        Stock = stock;
        Category = category.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity < 1) 
            throw new ArgumentException("Quantity must be positive");

        if (quantity > Stock)
            throw new InsufficientStockException(Name, Stock, quantity);

        Stock -= quantity;
    }
}