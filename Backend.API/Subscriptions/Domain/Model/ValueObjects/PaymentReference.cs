using System.Text.RegularExpressions;

namespace Backend.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Payment Reference Value Object
/// </summary>
public record PaymentReference
{
    private static readonly Regex ReferenceRegex = new(@"^[A-Z0-9\-_]+$", RegexOptions.Compiled);
    
    private const int MinLength = 5;
    private const int MaxLength = 100;

    public string Value { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PaymentReference" /> class.
    /// </summary>
    /// <param name="value">The payment reference</param>
    /// <exception cref="ArgumentException">Thrown when the reference is invalid</exception>
    public PaymentReference(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Payment reference cannot be empty", nameof(value));

        var cleanValue = value.Trim().ToUpperInvariant();

        if (cleanValue.Length < MinLength)
            throw new ArgumentException($"Payment reference must be at least {MinLength} characters long", nameof(value));

        if (cleanValue.Length > MaxLength)
            throw new ArgumentException($"Payment reference cannot exceed {MaxLength} characters", nameof(value));

        if (!ReferenceRegex.IsMatch(cleanValue))
            throw new ArgumentException("Payment reference can only contain letters, numbers, hyphens and underscores", nameof(value));

        Value = cleanValue;
    }

    /// <summary>
    ///     Generates a new payment reference with a prefix
    /// </summary>
    public static PaymentReference Generate(string prefix = "PAY")
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var random = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
        return new PaymentReference($"{prefix}-{timestamp}-{random}");
    }

    /// <summary>
    ///     Creates a payment reference from a transaction ID
    /// </summary>
    public static PaymentReference FromTransactionId(string transactionId)
    {
        return new PaymentReference(transactionId);
    }

    public static implicit operator string(PaymentReference reference) => reference.Value;
    public static explicit operator PaymentReference(string value) => new(value);

    public override string ToString() => Value;
}
