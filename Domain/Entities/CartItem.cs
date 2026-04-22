using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Domain.Entities;

public class CartItem
{
    public ProductId ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; private set; }
    public Money PricePerUnit { get; }

    public CartItem(ProductId productId, string productName, int quantity, Money pricePerUnit)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
    }

    public void IncreaseQuantity(int amount) => Quantity += amount;
    public Money GetTotal() => PricePerUnit * Quantity;
}