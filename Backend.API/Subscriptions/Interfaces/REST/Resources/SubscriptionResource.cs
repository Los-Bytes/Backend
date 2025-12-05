namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

public record SubscriptionResource(
    int Id,
    int UserId,
    string PlanType,
    DateTime StartDate,
    DateTime? EndDate,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive
);