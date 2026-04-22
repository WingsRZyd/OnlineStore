using OnlineStore.Domain.Entities;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(OrderId id);
    Task<List<Order>> GetByUserIdAsync(int userId = 1);
}