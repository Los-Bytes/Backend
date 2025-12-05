namespace Backend.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Subscription Status Value Object
/// </summary>
public record SubscriptionStatus
{
    public string Value { get; }

    private SubscriptionStatus(string value)
    {
        Value = value;
    }

    // Estados predefinidos
    public static readonly SubscriptionStatus Active = new("Active");
    public static readonly SubscriptionStatus Inactive = new("Inactive");
    public static readonly SubscriptionStatus Cancelled = new("Cancelled");
    public static readonly SubscriptionStatus Expired = new("Expired");
    public static readonly SubscriptionStatus Pending = new("Pending");

    /// <summary>
    ///     Creates a status from a string
    /// </summary>
    public static SubscriptionStatus FromString(string value)
    {
        return value?.ToUpperInvariant() switch
        {
            "ACTIVE" => Active,
            "INACTIVE" => Inactive,
            "CANCELLED" => Cancelled,
            "EXPIRED" => Expired,
            "PENDING" => Pending,
            _ => throw new ArgumentException($"Invalid subscription status: {value}", nameof(value))
        };
    }

    /// <summary>
    ///     Checks if the subscription can be activated
    /// </summary>
    public bool CanActivate() => this == Inactive || this == Pending;

    /// <summary>
    ///     Checks if the subscription can be cancelled
    /// </summary>
    public bool CanCancel() => this == Active;

    /// <summary>
    ///     Checks if the subscription can be renewed
    /// </summary>
    public bool CanRenew() => this == Expired || this == Inactive;

    /// <summary>
    ///     Checks if subscription is in a final state
    /// </summary>
    public bool IsFinalState() => this == Cancelled;

    public static implicit operator string(SubscriptionStatus status) => status.Value;

    public override string ToString() => Value;
}