namespace Backend.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Update Subscription Command
/// </summary>
/// <param name="Id">The subscription identifier</param>
/// <param name="MaxMembers">Maximum members allowed</param>
/// <param name="MaxInventoryItems">Maximum inventory items allowed</param>
/// <param name="IsActive">Whether the subscription is active</param>
public record UpdateSubscriptionCommand(
    int Id,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive);