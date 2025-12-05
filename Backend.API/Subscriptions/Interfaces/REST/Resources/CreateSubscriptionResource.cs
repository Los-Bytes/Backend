namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource for creating a new subscription
/// </summary>
/// <param name="UserId">The user identifier</param>
/// <param name="PlanType">The plan type (Free, Pro, Max)</param>
/// <param name="StartDate">The subscription start date</param>
/// <param name="EndDate">The subscription end date (nullable)</param>
/// <param name="MaxMembers">Maximum members allowed</param>
/// <param name="MaxInventoryItems">Maximum inventory items allowed</param>
/// <param name="IsActive">Whether the subscription is active</param>
public record CreateSubscriptionResource(
    int UserId,
    string PlanType,
    DateTime StartDate,
    DateTime? EndDate,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive);