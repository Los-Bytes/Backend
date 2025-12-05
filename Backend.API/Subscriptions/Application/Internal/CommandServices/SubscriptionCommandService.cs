using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Repositories;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Subscriptions.Application.Internal.CommandServices;

/// <summary>
///     Subscription command service
/// </summary>
/// <param name="subscriptionRepository">
///     Subscription repository
/// </param>
/// <param name="subscriptionPlanRepository">
///     Subscription plan repository
/// </param>
/// <param name="subscriptionQueryService">
///     Subscription query service
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPlanRepository subscriptionPlanRepository,
    ISubscriptionQueryService subscriptionQueryService,
    IUnitOfWork unitOfWork
) : ISubscriptionCommandService
{
    /// <inheritdoc />
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        var plan = await subscriptionPlanRepository.FindByNameAsync(command.PlanType);
        if (plan == null)
            return null;

        var existingActive = await subscriptionRepository.FindActiveByUserIdAsync(command.UserId);
        if (existingActive != null && existingActive.PlanType == command.PlanType)
            return null;

        var subscription = new Subscription(command, plan);
        
        try
        {
            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Subscription?> Handle(UpdateSubscriptionCommand command)
    {
        try
        {
            var query = new GetSubscriptionByIdQuery(command.Id);
            var subscription = await subscriptionQueryService.Handle(query);

            if (subscription == null)
                return null;

            subscription.UpdateLimits(command.MaxMembers, command.MaxInventoryItems);
            subscription.UpdateStatus(command.IsActive);
            
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command)
    {
        try
        {
            var newPlan = await subscriptionPlanRepository.FindByNameAsync(command.NewPlanType);
            if (newPlan == null)
                return null;

            var currentQuery = new GetActiveSubscriptionByUserIdQuery(command.UserId);
            var currentSubscription = await subscriptionQueryService.Handle(currentQuery);
            
            if (currentSubscription != null)
            {
                currentSubscription.Deactivate();
                subscriptionRepository.Update(currentSubscription);
            }

            var createCommand = new CreateSubscriptionCommand(
                command.UserId,
                command.NewPlanType,
                DateTime.UtcNow,
                null,
                newPlan.MaxMembers,
                newPlan.MaxInventoryItems,
                true
            );

            var newSubscription = new Subscription(createCommand, newPlan);
            await subscriptionRepository.AddAsync(newSubscription);
            await unitOfWork.CompleteAsync();

            return newSubscription;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Subscription?> InitializeDefaultSubscription(int userId)
    {
        try
        {
            var existing = await subscriptionRepository.FindActiveByUserIdAsync(userId);
            if (existing != null)
                return existing;

            var freePlan = await subscriptionPlanRepository.FindByNameAsync("Free");
            if (freePlan == null)
                return null;

            var command = new CreateSubscriptionCommand(
                userId,
                "Free",
                DateTime.UtcNow,
                null,
                freePlan.MaxMembers,
                freePlan.MaxInventoryItems,
                true
            );

            var subscription = new Subscription(command, freePlan);
            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();

            return subscription;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
