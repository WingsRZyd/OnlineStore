using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Application.UseCases;

public class AddItemToCartUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;

    public AddItemToCartUseCase(IProductRepository productRepository, ICartRepository cartRepository)
    {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
    }

    public async Task ExecuteAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(ProductId.From(productId));
        if (product == null)
            throw new ProductNotFoundException(productId);

        var cart = await _cartRepository.GetOrCreateAsync(1);
        cart.AddItem(product, quantity);
        await _cartRepository.SaveAsync(cart);
    }
}