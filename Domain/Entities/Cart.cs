using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Domain.Entities;

public class Cart
{
    public int UserId { get; }
    private readonly List<CartItem> _items = new();

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public Cart(int userId = 1)
    {
        UserId = userId;
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity < 1)
            throw new ArgumentException("Quantity must be at least 1.");

        var existing = _items.FirstOrDefault(x => x.ProductId == product.Id);

        if (existing != null)
            existing.IncreaseQuantity(quantity);
        else
            _items.Add(new CartItem(product.Id, product.Name, quantity, product.Price));
    }

    public void Clear() => _items.Clear();
    public Money GetTotal() => _items.Aggregate(Money.Zero, (sum, item) => sum + item.GetTotal());
}