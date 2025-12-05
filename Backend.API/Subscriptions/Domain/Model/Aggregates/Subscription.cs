using Backend.API.Subscriptions.Domain.Model.Commands;

namespace Backend.API.Subscriptions.Domain.Model.Aggregates;

/// <summary>
///     Subscription Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Subscription aggregate root for LabIoT.
///     It manages user subscription to plans (Free, Pro, Max) with usage limits.
/// </remarks>
public partial class Subscription
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Subscription" /> class with default values.
    /// </summary>
    public Subscription()
    {
        PlanType = "Free";
        StartDate = DateTime.UtcNow;
        MaxMembers = 3;
        MaxInventoryItems = 50;
        IsActive = true;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Subscription" /> class from a command.
    /// </summary>
    /// <param name="command">The create subscription command</param>
    /// <param name="plan">The subscription plan</param>
    public Subscription(CreateSubscriptionCommand command, SubscriptionPlan plan)
    {
        UserId = command.UserId;
        PlanType = command.PlanType;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        MaxMembers = plan.MaxMembers;
        MaxInventoryItems = plan.MaxInventoryItems;
        IsActive = command.IsActive;
    }

    /// <summary>
    ///     Gets the unique identifier of the subscription.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Gets the user identifier.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    ///     Gets the plan type (Free, Pro, Max).
    /// </summary>
    public string PlanType { get; private set; }

    /// <summary>
    ///     Gets the subscription start date.
    /// </summary>
    public DateTime StartDate { get; private set; }

    /// <summary>
    ///     Gets the subscription end date.
    /// </summary>
    public DateTime? EndDate { get; private set; }

    /// <summary>
    ///     Gets the maximum number of members allowed.
    /// </summary>
    public int MaxMembers { get; private set; }

    /// <summary>
    ///     Gets the maximum number of inventory items allowed.
    /// </summary>
    public int MaxInventoryItems { get; private set; }

    /// <summary>
    ///     Gets whether the subscription is active.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    ///     Deactivates the subscription.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        EndDate = DateTime.UtcNow;
    }

    /// <summary>
    ///     Updates subscription limits.
    /// </summary>
    /// <param name="maxMembers">Maximum members</param>
    /// <param name="maxInventoryItems">Maximum inventory items</param>
    public void UpdateLimits(int maxMembers, int maxInventoryItems)
    {
        MaxMembers = maxMembers;
        MaxInventoryItems = maxInventoryItems;
    }

    /// <summary>
    ///     Updates subscription status.
    /// </summary>
    /// <param name="isActive">Active status</param>
    public void UpdateStatus(bool isActive)
    {
        IsActive = isActive;
        if (!isActive && !EndDate.HasValue)
            EndDate = DateTime.UtcNow;
    }

    /// <summary>
    ///     Checks if the subscription is expired.
    /// </summary>
    public bool IsExpired()
    {
        return EndDate.HasValue && EndDate.Value < DateTime.UtcNow;
    }

    /// <summary>
    ///     Checks if members are unlimited.
    /// </summary>
    public bool HasUnlimitedMembers()
    {
        return MaxMembers == -1;
    }

    /// <summary>
    ///     Checks if inventory items are unlimited.
    /// </summary>
    public bool HasUnlimitedItems()
    {
        return MaxInventoryItems == -1;
    }
}
