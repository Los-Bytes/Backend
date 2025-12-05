namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource for changing subscription plan
/// </summary>
/// <param name="UserId">The user identifier</param>
/// <param name="NewPlanType">The new plan type (Free, Pro, Max)</param>
public record ChangeSubscriptionPlanResource(
    int UserId,
    string NewPlanType);