using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetOrCreateAsync(int userId = 1);
    Task SaveAsync(Cart cart);
}