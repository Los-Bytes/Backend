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
public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionQueryService subscriptionQueryService,
    IUnitOfWork unitOfWork
) : ISubscriptionCommandService
{
    /// <inheritdoc />
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        try
        {
            var subscription = new Subscription(command);
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

            if (subscription == null) return null;

            subscription.UpdatePrice(command.Amount, command.Currency);
            
            if (command.IsActive)
                subscription.Activate();
            else
                subscription.Cancel();
            
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
            // Cancelar suscripción actual
            var currentQuery = new GetActiveSubscriptionByUserIdQuery(command.UserId);
            var currentSubscription = await subscriptionQueryService.Handle(currentQuery);
            
            if (currentSubscription != null)
            {
                currentSubscription.Cancel();
                subscriptionRepository.Update(currentSubscription);
            }

            // Determinar precio según el plan
            decimal amount = command.NewPlanType.ToUpperInvariant() switch
            {
                "FREE" => 0m,
                "PRO" => 9.99m,
                "MAX" => 19.99m,
                _ => 0m
            };

            // Crear nueva suscripción
            var createCommand = new CreateSubscriptionCommand(
                PlanId: 0, // No usamos PlanId
                UserId: command.UserId,
                Amount: amount,
                Currency: "USD",
                StartDate: DateTime.UtcNow,
                EndDate: amount == 0 ? DateTime.UtcNow.AddYears(100) : DateTime.UtcNow.AddMonths(1),
                PaymentReference: $"PLAN-{command.NewPlanType}-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Status: "Active"
            );

            var newSubscription = new Subscription(createCommand);
            await subscriptionRepository.AddAsync(newSubscription);
            await unitOfWork.CompleteAsync();

            return newSubscription;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
