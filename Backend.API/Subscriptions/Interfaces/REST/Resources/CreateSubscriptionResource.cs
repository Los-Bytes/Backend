namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Create Subscription Resource
/// </summary>
public record CreateSubscriptionResource(
    int PlanId,
    int UserId,
    decimal Amount,
    string Currency,
    DateTime StartDate,
    DateTime EndDate,
    string PaymentReference,
    string Status);