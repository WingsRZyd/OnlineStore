using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Repositories;

public class InMemoryCartRepository : ICartRepository
{
    private static Cart? _cart;   // ← static, чтобы состояние сохранялось

    public Task<Cart> GetOrCreateAsync(int userId = 1)
    {
        _cart ??= new Cart(userId);
        return Task.FromResult(_cart);
    }

    public Task SaveAsync(Cart cart)
    {
        _cart = cart;
        return Task.CompletedTask;
    }
}