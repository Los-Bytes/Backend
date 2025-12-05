namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource for updating a subscription
/// </summary>
/// <param name="MaxMembers">Maximum members allowed</param>
/// <param name="MaxInventoryItems">Maximum inventory items allowed</param>
/// <param name="IsActive">Whether the subscription is active</param>
public record UpdateSubscriptionResource(
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive);