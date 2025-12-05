using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Backend.API.Subscriptions.Domain.Model.Commands;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Subscriptions.Interfaces.REST.Resources;
using Backend.API.Subscriptions.Interfaces.REST.Transform;

namespace Backend.API.Subscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptionsByUserId([FromQuery] int userId)
    {
        var query = new GetAllSubscriptionsByUserIdQuery(userId);
        var subscriptions = await subscriptionQueryService.Handle(query);
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveSubscriptionByUserId([FromQuery] int userId)
    {
        var query = new GetActiveSubscriptionByUserIdQuery(userId);
        var subscription = await subscriptionQueryService.Handle(query);
        
        if (subscription == null)
            return NotFound(new { message = $"No active subscription found for user {userId}." });
        
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        var query = new GetSubscriptionByIdQuery(id);
        var subscription = await subscriptionQueryService.Handle(query);
        
        if (subscription == null)
            return NotFound(new { message = $"Subscription with ID {id} not found." });
        
        var resource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource)
    {
        try
        {
            var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
            var subscription = await subscriptionCommandService.Handle(command);
            
            if (subscription == null)
                return BadRequest(new { message = "Failed to create subscription." });
            
            var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
            return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscriptionResource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSubscription(int id, [FromBody] UpdateSubscriptionResource resource)
    {
        try
        {
            var command = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var subscription = await subscriptionCommandService.Handle(command);
            
            if (subscription == null)
                return NotFound(new { message = $"Subscription with ID {id} not found." });
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("change-plan")]
    public async Task<IActionResult> ChangeSubscriptionPlan([FromBody] ChangeSubscriptionPlanResource resource)
    {
        try
        {
            var command = ChangeSubscriptionPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
            var subscription = await subscriptionCommandService.Handle(command);
            
            if (subscription == null)
                return BadRequest(new { message = "Failed to change subscription plan." });
            
            var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
            return Ok(subscriptionResource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
