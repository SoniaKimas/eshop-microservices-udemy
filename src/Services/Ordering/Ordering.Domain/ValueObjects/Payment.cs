namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardNumber { get; } = default!;
    public string? CardName { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment()
    {
    }

    private Payment(string cardNumber, string cardName, string expiration, string cvv, int paymentMethod)
    {
        CardNumber = cardNumber;
        CardName = cardName;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardNumber, string cardName, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration, nameof(expiration));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);
        return new Payment(cardNumber, cardName, expiration, cvv, paymentMethod);
    }
}