namespace OnlineStore.Domain.ValueObjects;

public record Money(decimal Amount)
{
    public static Money Zero => new(0);

    public static Money From(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");
        return new Money(amount);
    }

    public static Money operator *(Money money, int quantity) => 
        new Money(money.Amount * quantity);

    public static Money operator +(Money a, Money b) => 
        new Money(a.Amount + b.Amount);
}