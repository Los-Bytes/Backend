namespace Backend.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Subscription Period Value Object
/// </summary>
public record SubscriptionPeriod
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SubscriptionPeriod" /> class.
    /// </summary>
    /// <param name="startDate">The subscription start date</param>
    /// <param name="endDate">The subscription end date</param>
    /// <exception cref="ArgumentException">Thrown when dates are invalid</exception>
    public SubscriptionPeriod(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date", nameof(startDate));

        // Normalizar a UTC
        StartDate = startDate.Kind == DateTimeKind.Utc ? startDate : DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        EndDate = endDate.Kind == DateTimeKind.Utc ? endDate : DateTime.SpecifyKind(endDate, DateTimeKind.Utc);
    }

    /// <summary>
    ///     Creates a subscription period from a start date and duration in months
    /// </summary>
    public static SubscriptionPeriod FromMonths(DateTime startDate, int months)
    {
        if (months <= 0)
            throw new ArgumentException("Months must be positive", nameof(months));

        var endDate = startDate.AddMonths(months);
        return new SubscriptionPeriod(startDate, endDate);
    }

    /// <summary>
    ///     Creates a subscription period from today for a given number of months
    /// </summary>
    public static SubscriptionPeriod StartingToday(int months)
    {
        return FromMonths(DateTime.UtcNow, months);
    }

    /// <summary>
    ///     Calculates the duration in days
    /// </summary>
    public int DurationInDays => (EndDate - StartDate).Days;

    /// <summary>
    ///     Calculates the duration in months (approximate)
    /// </summary>
    public int DurationInMonths => (int)Math.Round((EndDate - StartDate).TotalDays / 30.44);

    /// <summary>
    ///     Checks if the subscription is currently active
    /// </summary>
    public bool IsActive(DateTime? referenceDate = null)
    {
        var now = referenceDate ?? DateTime.UtcNow;
        return now >= StartDate && now <= EndDate;
    }

    /// <summary>
    ///     Checks if the subscription has expired
    /// </summary>
    public bool HasExpired(DateTime? referenceDate = null)
    {
        var now = referenceDate ?? DateTime.UtcNow;
        return now > EndDate;
    }

    /// <summary>
    ///     Checks if the subscription will start in the future
    /// </summary>
    public bool IsFuture(DateTime? referenceDate = null)
    {
        var now = referenceDate ?? DateTime.UtcNow;
        return now < StartDate;
    }

    /// <summary>
    ///     Calculates remaining days
    /// </summary>
    public int RemainingDays(DateTime? referenceDate = null)
    {
        var now = referenceDate ?? DateTime.UtcNow;
        if (now > EndDate) return 0;
        if (now < StartDate) return DurationInDays;
        return (EndDate - now).Days;
    }

    /// <summary>
    ///     Extends the subscription by adding months to the end date
    /// </summary>
    public SubscriptionPeriod Extend(int months)
    {
        if (months <= 0)
            throw new ArgumentException("Extension must be positive", nameof(months));

        return new SubscriptionPeriod(StartDate, EndDate.AddMonths(months));
    }

    /// <summary>
    ///     Renews the subscription starting from the end date
    /// </summary>
    public SubscriptionPeriod Renew(int months)
    {
        return FromMonths(EndDate, months);
    }

    public override string ToString() => $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd} ({DurationInDays} days)";
}
