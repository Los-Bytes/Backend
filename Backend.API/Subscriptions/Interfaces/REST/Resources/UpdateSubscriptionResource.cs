namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Update Subscription Resource
/// </summary>
public record UpdateSubscriptionResource(
    decimal Amount,
    string Currency,
    bool IsActive);