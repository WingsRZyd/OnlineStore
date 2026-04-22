namespace OnlineStore.Models;

public class CreateOrderRequest
{
    public string DeliveryAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}