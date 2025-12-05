namespace Backend.API.Subscriptions.Interfaces.REST.Resources;

/// <summary>
///     Resource for subscription plan
/// </summary>
/// <param name="Id">The plan identifier</param>
/// <param name="Name">The plan name</param>
/// <param name="Price">The plan price</param>
/// <param name="Currency">The currency</param>
/// <param name="Period">The billing period</param>
/// <param name="MaxMembers">Maximum members allowed</param>
/// <param name="MaxInventoryItems">Maximum inventory items allowed</param>
/// <param name="Features">The plan features</param>
public record SubscriptionPlanResource(
    int Id,
    string Name,
    decimal Price,
    string Currency,
    string Period,
    int MaxMembers,
    int MaxInventoryItems,
    List<string> Features);