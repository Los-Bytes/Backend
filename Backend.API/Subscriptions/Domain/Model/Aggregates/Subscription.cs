using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Domain.Model.ValueObjects;

namespace Backend.API.Subscriptions.Domain.Model.Aggregates;

/// <summary>
///     Subscription Aggregate Root
/// </summary>
public partial class Subscription
{
    private DateTime _startDate;
    private DateTime _endDate;

    // Constructores
    public Subscription()
    {
        Price = SubscriptionPrice.Free();
        var period = SubscriptionPeriod.StartingToday(1);
        _startDate = period.StartDate;
        _endDate = period.EndDate;
        PaymentReference = PaymentReference.Generate();
        Status = SubscriptionStatus.Pending;
        BillingCycle = BillingCycle.Monthly;
    }

    public Subscription(CreateSubscriptionCommand command)
    {
        PlanId = command.PlanId;
        UserId = command.UserId;
        
        Price = new SubscriptionPrice(command.Amount, command.Currency);
        _startDate = command.StartDate;
        _endDate = command.EndDate;
        PaymentReference = new PaymentReference(command.PaymentReference);
        Status = SubscriptionStatus.FromString(command.Status);
        BillingCycle = BillingCycle.Custom(CalculateMonths(command.StartDate, command.EndDate));
    }

    // Propiedades
    public int Id { get; }
    public int PlanId { get; private set; }
    public int UserId { get; private set; }
    
    public SubscriptionPrice Price { get; private set; }
    
    // ✅ Propiedad calculada (NO mapeada a BD)
    public SubscriptionPeriod Period 
    { 
        get => new SubscriptionPeriod(_startDate, _endDate);
        private set 
        {
            _startDate = value.StartDate;
            _endDate = value.EndDate;
        }
    }
    
    public PaymentReference PaymentReference { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public BillingCycle BillingCycle { get; private set; }

    // Métodos de negocio
    public void Activate()
    {
        if (!Status.CanActivate())
            throw new InvalidOperationException($"Cannot activate subscription in {Status} state");
        Status = SubscriptionStatus.Active;
    }

    public void Cancel()
    {
        if (!Status.CanCancel())
            throw new InvalidOperationException($"Cannot cancel subscription in {Status} state");
        Status = SubscriptionStatus.Cancelled;
    }

    public void Renew(int months)
    {
        if (!Status.CanRenew())
            throw new InvalidOperationException($"Cannot renew subscription in {Status} state");
        Period = Period.Renew(months);
        Status = SubscriptionStatus.Active;
    }

    public void Extend(int months)
    {
        Period = Period.Extend(months);
    }

    public void UpdatePrice(decimal amount, string currency = "USD")
    {
        Price = new SubscriptionPrice(amount, currency);
    }

    public void ApplyDiscount(decimal discountPercentage)
    {
        Price = Price.ApplyDiscount(discountPercentage);
    }

    public bool IsCurrentlyActive() => Period.IsActive() && Status == SubscriptionStatus.Active;
    public bool HasExpired() => Period.HasExpired();
    public int GetRemainingDays() => Period.RemainingDays();

    public void CheckAndMarkAsExpired()
    {
        if (HasExpired() && Status == SubscriptionStatus.Active)
        {
            Status = SubscriptionStatus.Expired;
        }
    }

    private static int CalculateMonths(DateTime start, DateTime end)
    {
        return ((end.Year - start.Year) * 12) + end.Month - start.Month;
    }
}
