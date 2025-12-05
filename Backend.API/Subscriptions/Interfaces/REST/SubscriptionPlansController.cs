using System.Net.Mime;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Subscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Subscriptions.Interfaces.REST;

/// <summary>
///     Controller for managing subscription plans.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Subscription Plan Endpoints.")]
public class SubscriptionPlansController(ISubscriptionPlanQueryService planQueryService)
    : ControllerBase
{
    /// <summary>
    ///     Get all subscription plans
    /// </summary>
    [HttpGet]
    [SwaggerOperation("Get All Plans", "Get all available subscription plans.", 
        OperationId = "GetAllPlans")]
    [SwaggerResponse(200, "The plans were found and returned.")]
    public async Task<IActionResult> GetAllPlans()
    {
        var query = new GetAllPlansQuery();
        var plans = await planQueryService.Handle(query);
        var resources = plans.Select(SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    ///     Get a subscription plan by name
    /// </summary>
    [HttpGet("{name}")]
    [SwaggerOperation("Get Plan by Name", "Get a subscription plan by its name.", 
        OperationId = "GetPlanByName")]
    [SwaggerResponse(200, "The plan was found and returned.")]
    [SwaggerResponse(404, "The plan was not found.")]
    public async Task<IActionResult> GetPlanByName(string name)
    {
        var query = new GetPlanByNameQuery(name);
        var plan = await planQueryService.Handle(query);
        if (plan is null) return NotFound(new { message = $"Plan '{name}' not found." });
        var resource = SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity(plan);
        return Ok(resource);
    }
}