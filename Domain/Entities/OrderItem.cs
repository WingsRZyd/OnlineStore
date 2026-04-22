using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Domain.Entities;

public class OrderItem
{
    public ProductId ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public Money PricePerUnit { get; }

    public OrderItem(CartItem cartItem)
    {
        ProductId = cartItem.ProductId;
        ProductName = cartItem.ProductName;
        Quantity = cartItem.Quantity;
        PricePerUnit = cartItem.PricePerUnit;
    }

    public Money GetTotal() => PricePerUnit * Quantity;
}