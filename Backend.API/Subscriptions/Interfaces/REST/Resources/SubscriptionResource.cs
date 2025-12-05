namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource for subscription
/// </summary>
/// <param name="Id">The subscription identifier</param>
/// <param name="UserId">The user identifier</param>
/// <param name="PlanType">The plan type</param>
/// <param name="StartDate">The subscription start date</param>
/// <param name="EndDate">The subscription end date</param>
/// <param name="MaxMembers">Maximum members allowed</param>
/// <param name="MaxInventoryItems">Maximum inventory items allowed</param>
/// <param name="IsActive">Whether the subscription is active</param>
public record SubscriptionResource(
    int Id,
    int UserId,
    string PlanType,
    DateTime StartDate,
    DateTime? EndDate,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive);