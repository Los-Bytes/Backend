using System.Net.Mime;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Subscriptions.Interfaces.REST.Resources;
using Backend.API.Subscriptions.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Subscriptions.Interfaces.REST;

/// <summary>
///     Controller for managing subscriptions
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
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Subscription by Id")]
    [SwaggerResponse(200, "Subscription found", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        var query = new GetSubscriptionByIdQuery(id);
        var subscription = await subscriptionQueryService.Handle(query);
        if (subscription is null) return NotFound();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation("Get Active Subscription by User Id")]
    [SwaggerResponse(200, "Subscription found", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> GetActiveSubscriptionByUserId(int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        if (subscription is null) return NotFound();
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    [HttpGet("user/{userId:int}/all")]
    [SwaggerOperation("Get All Subscriptions by User Id")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    public async Task<IActionResult> GetAllSubscriptionsByUserId(int userId)
    {
        var query = new GetAllSubscriptionsByUserIdQuery(userId);
        var subscriptions = await subscriptionQueryService.Handle(query);
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation("Create Subscription")]
    [SwaggerResponse(201, "Subscription created", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "Bad request")]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionResource resource)
    {
        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await subscriptionCommandService.Handle(command);
        if (subscription is null) return BadRequest();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscriptionResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update Subscription")]
    [SwaggerResponse(200, "Subscription updated", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> UpdateSubscription(int id, UpdateSubscriptionResource resource)
    {
        var command = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var subscription = await subscriptionCommandService.Handle(command);
        if (subscription is null) return NotFound();
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(subscriptionResource);
    }
}
