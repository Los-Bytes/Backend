namespace Backend.API.Inventory.Domain.Model.Queries;

/// <summary>
///     Get Assets by Responsible User Query
/// </summary>
/// <param name="ResponsibleUserId">The user profile id to filter by</param>
public record GetAssetsByResponsibleUserQuery(int ResponsibleUserId);