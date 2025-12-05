using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Backend.API.Subscriptions.Domain.Model.Queries;
using Backend.API.Subscriptions.Domain.Services;
using Backend.API.Subscriptions.Interfaces.REST.Transform;

namespace Backend.API.Subscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SubscriptionPlansController(ISubscriptionPlanQueryService planQueryService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPlans()
    {
        var query = new GetAllPlansQuery();
        var plans = await planQueryService.Handle(query);
        var resources = plans.Select(SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetPlanByName(string name)
    {
        var query = new GetPlanByNameQuery(name);
        var plan = await planQueryService.Handle(query);
        
        if (plan == null)
            return NotFound(new { message = $"Plan '{name}' not found." });
        
        var resource = SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity(plan);
        return Ok(resource);
    }
}