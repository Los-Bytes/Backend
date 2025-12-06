namespace Backend.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Billing Cycle Value Object
/// </summary>
public record BillingCycle
{
    public int Months { get; }
    public string Description { get; }

    private BillingCycle(int months, string description)
    {
        if (months <= 0)
            throw new ArgumentException("Billing cycle must be positive", nameof(months));

        Months = months;
        Description = description;
    }

    // Ciclos predefinidos
    public static readonly BillingCycle Monthly = new(1, "Monthly");
    public static readonly BillingCycle Quarterly = new(3, "Quarterly");
    public static readonly BillingCycle Biannual = new(6, "Biannual");
    public static readonly BillingCycle Annual = new(12, "Annual");

    /// <summary>
    ///     Creates a custom billing cycle
    /// </summary>
    public static BillingCycle Custom(int months)
    {
        return months switch
        {
            1 => Monthly,
            3 => Quarterly,
            6 => Biannual,
            12 => Annual,
            _ => new BillingCycle(months, $"{months} months")
        };
    }

    /// <summary>
    ///     Calculates the next billing date from a given date
    /// </summary>
    public DateTime NextBillingDate(DateTime fromDate)
    {
        return fromDate.AddMonths(Months);
    }

    /// <summary>
    ///     Calculates how many billing cycles fit in a period
    /// </summary>
    public int CyclesInPeriod(SubscriptionPeriod period)
    {
        return period.DurationInMonths / Months;
    }

    public override string ToString() => Description;
}