namespace OnlineStore.Domain.Exceptions;

public class ProductNotFoundException : DomainException
{
    public ProductNotFoundException(Guid productId)
        : base($"Product with id {productId} was not found")
    {
    }
}