using System.Net.Mime;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Subscriptions.Interfaces.REST.Resources;
using Backend.API.Subscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Subscriptions.Interfaces.REST;

/// <summary>
///     Controller for managing subscriptions.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Subscription Endpoints.")]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService)
    : ControllerBase
{
    /// <summary>
    ///     Get a subscription by its unique identifier
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Subscription by Id", "Get a subscription by its unique identifier.",
        OperationId = "GetSubscriptionById")]
    [SwaggerResponse(200, "The subscription was found and returned.", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "The subscription was not found.")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        var query = new GetSubscriptionByIdQuery(id);
        var subscription = await subscriptionQueryService.Handle(query);
        if (subscription is null) return NotFound();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    /// <summary>
    ///     Get active subscription by user ID
    /// </summary>
    [HttpGet("active")]
    [SwaggerOperation("Get Active Subscription by User Id", 
        "Get the active subscription for a user.", 
        OperationId = "GetActiveSubscriptionByUserId")]
    [SwaggerResponse(200, "The subscription was found and returned.", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "No active subscription found for user.")]
    public async Task<IActionResult> GetActiveSubscriptionByUserId([FromQuery] int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        if (subscription is null) 
            return NotFound(new { message = $"No active subscription found for user {userId}." });
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    /// <summary>
    ///     Get all subscriptions by user ID
    /// </summary>
    [HttpGet]
    [SwaggerOperation("Get All Subscriptions by User Id", 
        "Get all subscriptions for a user.", 
        OperationId = "GetAllSubscriptionsByUserId")]
    [SwaggerResponse(200, "The subscriptions were found and returned.", 
        typeof(IEnumerable<SubscriptionResource>))]
    public async Task<IActionResult> GetAllSubscriptionsByUserId([FromQuery] int userId)
    {
        var query = new GetAllSubscriptionsByUserIdQuery(userId);
        var subscriptions = await subscriptionQueryService.Handle(query);
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    ///     Create a new subscription
    /// </summary>
    [HttpPost]
    [SwaggerOperation("Create Subscription", "Create a new subscription.", 
        OperationId = "CreateSubscription")]
    [SwaggerResponse(201, "The subscription was created.", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "The subscription was not created.")]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionResource resource)
    {
        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await subscriptionCommandService.Handle(command);
        if (subscription is null) return BadRequest();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id },
            subscriptionResource);
    }

    /// <summary>
    ///     Update subscription limits
    /// </summary>
    [HttpPut("{id:int}")]
    [SwaggerOperation("Update Subscription", "Update subscription limits and status.", 
        OperationId = "UpdateSubscription")]
    [SwaggerResponse(200, "The subscription was updated.", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "The subscription was not found.")]
    public async Task<IActionResult> UpdateSubscription(int id, UpdateSubscriptionResource resource)
    {
        var command = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var subscription = await subscriptionCommandService.Handle(command);
        if (subscription is null) return NotFound();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(subscriptionResource);
    }

    /// <summary>
    ///     Change subscription plan
    /// </summary>
    [HttpPost("change-plan")]
    [SwaggerOperation("Change Subscription Plan", 
        "Change user subscription to a different plan.", 
        OperationId = "ChangeSubscriptionPlan")]
    [SwaggerResponse(200, "The subscription plan was changed.", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "The subscription plan was not changed.")]
    public async Task<IActionResult> ChangeSubscriptionPlan(ChangeSubscriptionPlanResource resource)
    {
        var command = ChangeSubscriptionPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await subscriptionCommandService.Handle(command);
        if (subscription is null) return BadRequest();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(subscriptionResource);
    }
}
