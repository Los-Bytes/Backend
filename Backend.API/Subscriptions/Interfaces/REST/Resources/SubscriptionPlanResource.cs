namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

public record SubscriptionPlanResource(
    int Id,
    string Name,
    decimal Price,
    string Currency,
    string Period,
    int MaxMembers,
    int MaxInventoryItems,
    List<string> Features
);