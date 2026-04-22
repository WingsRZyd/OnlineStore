using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.UseCases;

public class GetCartUseCase
{
    private readonly ICartRepository _cartRepository;

    public GetCartUseCase(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> ExecuteAsync(int userId = 1)
    {
        return await _cartRepository.GetOrCreateAsync(userId);
    }
}