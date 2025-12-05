namespace Backend.API.Subscriptions.Domain.Model.Commands;

public record ChangeSubscriptionPlanCommand(
    int UserId,
    string NewPlanType
);