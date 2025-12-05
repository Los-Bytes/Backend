using Backend.API.Subscriptions.Domain.Model.Aggregates;
using Backend.API.Subscriptions.Domain.Model.Queries;

namespace Backend.API.Subscriptions.Domain.Services;

public interface ISubscriptionPlanQueryService
{
    Task<IEnumerable<SubscriptionPlan>> Handle(GetAllPlansQuery query);
    Task<SubscriptionPlan?> Handle(GetPlanByNameQuery query);
}