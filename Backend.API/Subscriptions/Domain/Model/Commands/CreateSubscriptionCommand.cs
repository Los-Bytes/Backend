namespace Backend.API.Subscriptions.Domain.Model.Commands;

public record CreateSubscriptionCommand(
    int UserId,
    string PlanType,
    DateTime StartDate,
    DateTime? EndDate,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive
);