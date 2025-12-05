namespace Backend.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Update Subscription Command
/// </summary>
public record UpdateSubscriptionCommand(
    int Id,
    decimal Amount,
    string Currency,
    bool IsActive);