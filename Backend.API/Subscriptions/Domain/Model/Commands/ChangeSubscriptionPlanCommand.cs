namespace Backend.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Change Subscription Plan Command
/// </summary>
public record ChangeSubscriptionPlanCommand(
    int UserId,
    string NewPlanType);