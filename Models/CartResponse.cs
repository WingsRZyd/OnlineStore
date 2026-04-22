namespace OnlineStore.Models;

public class CartResponse
{
    public List<CartItemResponse> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
}

public class CartItemResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}