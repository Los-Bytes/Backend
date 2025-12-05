namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

public record UpdateSubscriptionResource(
    int MaxMembers,
    int MaxInventoryItems,
    bool IsActive
);
