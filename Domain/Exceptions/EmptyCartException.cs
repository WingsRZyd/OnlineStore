namespace OnlineStore.Domain.Exceptions;

public class EmptyCartException : DomainException
{
    public EmptyCartException() : base("Cannot create order from empty cart")
    {
    }
}