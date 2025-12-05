using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Subscriptions.Application.Internal.CommandServices;

public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPlanRepository planRepository,
    IUnitOfWork unitOfWork) 
    : ISubscriptionCommandService
{
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        var plan = await planRepository.FindByNameAsync(command.PlanType);
        if (plan == null)
            throw new Exception($"Plan '{command.PlanType}' not found");

        var existingActive = await subscriptionRepository.FindActiveByUserIdAsync(command.UserId);
        if (existingActive != null && existingActive.PlanType == command.PlanType)
            throw new Exception($"User already has an active subscription to plan '{command.PlanType}'");

        var subscription = Subscription.Create(
            command.UserId,
            command.PlanType,
            command.StartDate,
            command.MaxMembers,
            command.MaxInventoryItems
        );

        await subscriptionRepository.AddAsync(subscription);
        await unitOfWork.CompleteAsync();

        return subscription;
    }

    public async Task<Subscription?> Handle(UpdateSubscriptionCommand command)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.Id);
        if (subscription == null)
            return null;

        subscription.Update(command.MaxMembers, command.MaxInventoryItems, command.IsActive);

        subscriptionRepository.Update(subscription);
        await unitOfWork.CompleteAsync();

        return subscription;
    }

    public async Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command)
    {
        var newPlan = await planRepository.FindByNameAsync(command.NewPlanType);
        if (newPlan == null)
            throw new Exception($"Plan '{command.NewPlanType}' not found");

        var currentSubscription = await subscriptionRepository.FindActiveByUserIdAsync(command.UserId);
        if (currentSubscription != null)
        {
            currentSubscription.Deactivate();
            subscriptionRepository.Update(currentSubscription);
        }

        var newSubscription = Subscription.Create(
            command.UserId,
            command.NewPlanType,
            DateTime.UtcNow,
            newPlan.MaxMembers,
            newPlan.MaxInventoryItems
        );

        await subscriptionRepository.AddAsync(newSubscription);
        await unitOfWork.CompleteAsync();

        return newSubscription;
    }

    public async Task<Subscription?> InitializeDefaultSubscription(int userId)
    {
        var existing = await subscriptionRepository.FindActiveByUserIdAsync(userId);
        if (existing != null)
            return existing;

        var freePlan = await planRepository.FindByNameAsync("Free");
        if (freePlan == null)
            throw new Exception("Free plan not found in database");

        var subscription = Subscription.Create(
            userId,
            "Free",
            DateTime.UtcNow,
            freePlan.MaxMembers,
            freePlan.MaxInventoryItems
        );

        await subscriptionRepository.AddAsync(subscription);
        await unitOfWork.CompleteAsync();

        return subscription;
    }
}
