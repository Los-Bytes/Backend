using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Commands;

namespace Backend.API.Subscriptions.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<Subscription?> Handle(CreateSubscriptionCommand command);
    Task<Subscription?> Handle(UpdateSubscriptionCommand command);
    Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command);
    Task<Subscription?> InitializeDefaultSubscription(int userId);
}