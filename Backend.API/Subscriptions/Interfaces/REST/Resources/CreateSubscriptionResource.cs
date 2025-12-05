namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

public record CreateSubscriptionResource(
    int UserId,
    string PlanType,
    DateTime StartDate,
    DateTime? EndDate,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive
);