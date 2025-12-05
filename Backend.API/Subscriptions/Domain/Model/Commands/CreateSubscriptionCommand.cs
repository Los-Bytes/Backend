namespace Backend.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Create Subscription Command
/// </summary>
public record CreateSubscriptionCommand(
    int PlanId,
    int UserId,
    decimal Amount,
    string Currency,
    DateTime StartDate,
    DateTime EndDate,
    string PaymentReference,
    string Status);