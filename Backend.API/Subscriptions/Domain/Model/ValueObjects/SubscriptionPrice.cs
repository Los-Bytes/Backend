namespace Backend.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Subscription Price Value Object
/// </summary>
public record SubscriptionPrice
{
    private const decimal MinPrice = 0;
    private const decimal MaxPrice = 1000000;

    public decimal Amount { get; }
    public string Currency { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SubscriptionPrice" /> class.
    /// </summary>
    /// <param name="amount">The price amount</param>
    /// <param name="currency">The currency code (USD, PEN, EUR, etc.)</param>
    /// <exception cref="ArgumentException">Thrown when the price is invalid</exception>
    public SubscriptionPrice(decimal amount, string currency = "USD")
    {
        if (amount < MinPrice)
            throw new ArgumentException($"Price cannot be negative", nameof(amount));

        if (amount > MaxPrice)
            throw new ArgumentException($"Price cannot exceed {MaxPrice}", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));

        if (currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter ISO code (e.g., USD, PEN, EUR)", nameof(currency));

        Amount = Math.Round(amount, 2); // Redondear a 2 decimales
        Currency = currency.ToUpperInvariant();
    }

    /// <summary>
    ///     Creates a free subscription price
    /// </summary>
    public static SubscriptionPrice Free() => new(0, "USD");

    /// <summary>
    ///     Creates a price from a decimal value (USD by default)
    /// </summary>
    public static SubscriptionPrice FromDecimal(decimal amount, string currency = "USD") 
        => new(amount, currency);

    /// <summary>
    ///     Checks if the subscription is free
    /// </summary>
    public bool IsFree() => Amount == 0;

    /// <summary>
    ///     Adds two prices (must have same currency)
    /// </summary>
    public SubscriptionPrice Add(SubscriptionPrice other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add prices with different currencies: {Currency} and {other.Currency}");

        return new SubscriptionPrice(Amount + other.Amount, Currency);
    }

    /// <summary>
    ///     Applies a discount percentage
    /// </summary>
    public SubscriptionPrice ApplyDiscount(decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Discount percentage must be between 0 and 100", nameof(discountPercentage));

        var discountedAmount = Amount * (1 - discountPercentage / 100);
        return new SubscriptionPrice(discountedAmount, Currency);
    }

    public static implicit operator decimal(SubscriptionPrice price) => price.Amount;
    
    public override string ToString() => $"{Amount:N2} {Currency}";
}
