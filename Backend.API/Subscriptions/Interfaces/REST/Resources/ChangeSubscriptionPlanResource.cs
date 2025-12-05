namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

public record ChangeSubscriptionPlanResource(
    int UserId,
    string NewPlanType
);