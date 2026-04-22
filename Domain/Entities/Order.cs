using OnlineStore.Domain.ValueObjects;
using OnlineStore.Domain.Exceptions;

namespace OnlineStore.Domain.Entities;

public class Order
{
    public OrderId Id { get; private set; }
    public int UserId { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public Money TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public string DeliveryAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { }

    public static Order Create(int userId, Cart cart, string deliveryAddress, string paymentMethod)
    {
        if (!cart.Items.Any())
            throw new EmptyCartException();

        var order = new Order
        {
            Id = OrderId.New(),
            UserId = userId,
            DeliveryAddress = deliveryAddress,
            PaymentMethod = paymentMethod,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in cart.Items)
        {
            order.Items.Add(new OrderItem(item));
        }

        order.TotalAmount = order.Items.Aggregate(Money.Zero, (sum, item) => sum + item.GetTotal());

        return order;
    }

    public void ConfirmPayment() => Status = OrderStatus.Paid;
}       