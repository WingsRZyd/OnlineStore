using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Application.UseCases;

public class CreateOrderUseCase
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderUseCase(
        ICartRepository cartRepository, 
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Order> ExecuteAsync(string deliveryAddress, string paymentMethod)
    {
        var cart = await _cartRepository.GetOrCreateAsync(1);
        
        foreach (var item in cart.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new ProductNotFoundException(item.ProductId.Value);

            product.DecreaseStock(item.Quantity);
        }

        var order = Order.Create(1, cart, deliveryAddress, paymentMethod);
        
        await _orderRepository.AddAsync(order);
        cart.Clear();
        await _cartRepository.SaveAsync(cart);

        return order;
    }
}