namespace Backend.API.Subscriptions.Domain.Model.Commands;

public record UpdateSubscriptionCommand(
    int Id,
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive
);