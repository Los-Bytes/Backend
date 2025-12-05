namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Subscription Resource
/// </summary>
public record SubscriptionResource(
    int Id,
    int PlanId,
    int UserId,
    decimal Amount,
    string Currency,
    DateTime StartDate,
    DateTime EndDate,
    string PaymentReference,
    string Status);