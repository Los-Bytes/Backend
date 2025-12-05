namespace Backend.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Get Plan by Name Query
/// </summary>
/// <param name="Name">
///     The plan name (Free, Pro, Max)
/// </param>
public record GetPlanByNameQuery(string Name);