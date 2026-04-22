using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.ValueObjects;

namespace OnlineStore.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public Task AddAsync(Order order)
    {
        _orders.Add(order);
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(OrderId id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }

    public Task<List<Order>> GetByUserIdAsync(int userId = 1)
    {
        return Task.FromResult(_orders.Where(o => o.UserId == userId).ToList());
    }
}