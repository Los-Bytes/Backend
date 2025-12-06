using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Commands;

namespace Backend.API.Subscriptions.Domain.Services;

/// <summary>
///     Subscription command service interface
/// </summary>
public interface ISubscriptionCommandService
{
    Task<Subscription?> Handle(CreateSubscriptionCommand command);
    Task<Subscription?> Handle(UpdateSubscriptionCommand command);
    Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command);
}