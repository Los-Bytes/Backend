namespace Backend.API.Subscriptions.Domain.Model.Aggregates;

/// <summary>
///     Subscription Plan Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the available subscription plans (Free, Pro, Max).
/// </remarks>
public partial class SubscriptionPlan
{
    /// <summary>
    ///     Gets the unique identifier of the subscription plan.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Gets the plan name (Free, Pro, Max).
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    ///     Gets the plan price.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    ///     Gets the currency (USD).
    /// </summary>
    public string Currency { get; private set; } = "USD";

    /// <summary>
    ///     Gets the billing period (monthly, yearly).
    /// </summary>
    public string Period { get; private set; } = "monthly";

    /// <summary>
    ///     Gets the maximum number of members (-1 for unlimited).
    /// </summary>
    public int MaxMembers { get; private set; }

    /// <summary>
    ///     Gets the maximum number of inventory items (-1 for unlimited).
    /// </summary>
    public int MaxInventoryItems { get; private set; }

    /// <summary>
    ///     Gets the plan features as JSON string.
    /// </summary>
    public string Features { get; private set; } = "[]";

    /// <summary>
    ///     Checks if the plan is free.
    /// </summary>
    public bool IsFree()
    {
        return Price == 0;
    }

    /// <summary>
    ///     Checks if the plan is unlimited.
    /// </summary>
    public bool IsUnlimited()
    {
        return MaxMembers == -1 && MaxInventoryItems == -1;
    }

    /// <summary>
    ///     Gets formatted price.
    /// </summary>
    public string GetPriceFormatted()
    {
        return IsFree() ? "Gratis" : $"${Price:F2} {Currency}";
    }

    /// <summary>
    ///     Gets features as a list.
    /// </summary>
    public List<string> GetFeaturesList()
    {
        return System.Text.Json.JsonSerializer.Deserialize<List<string>>(Features) ?? new List<string>();
    }
}
