using System.Net.Mime;
using Backend.API.Laboratories.Domain.Model.Queries;
using Backend.API.Laboratories.Domain.Services;
using Backend.API.Laboratories.Interfaces.REST.Resources;
using Backend.API.Laboratories.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Laboratories.Interfaces.REST;

/// <summary>
///     Controller for managing laboratories
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Laboratory Endpoints.")]
public class LaboratoriesController(
    ILaboratoryCommandService laboratoryCommandService,
    ILaboratoryQueryService laboratoryQueryService)
    : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Laboratory by Id")]
    [SwaggerResponse(200, "Laboratory found", typeof(LaboratoryResource))]
    [SwaggerResponse(404, "Laboratory not found")]
    public async Task<IActionResult> GetLaboratoryById(int id)
    {
        var query = new GetLaboratoryByIdQuery(id);
        var laboratory = await laboratoryQueryService.Handle(query);
        if (laboratory is null) return NotFound();
        var resource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Laboratories")]
    [SwaggerResponse(200, "Laboratories found", typeof(IEnumerable<LaboratoryResource>))]
    public async Task<IActionResult> GetAllLaboratories([FromQuery] int? userId)
    {
        IEnumerable<Backend.API.Laboratories.Domain.Model.Aggregates.Laboratory> laboratories;

        if (userId.HasValue)
        {
            var query = new GetLaboratoriesByMemberIdQuery(userId.Value);
            laboratories = await laboratoryQueryService.Handle(query);
        }
        else
        {
            var query = new GetAllLaboratoriesQuery();
            laboratories = await laboratoryQueryService.Handle(query);
        }

        var resources = laboratories.Select(LaboratoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation("Create Laboratory")]
    [SwaggerResponse(201, "Laboratory created", typeof(LaboratoryResource))]
    [SwaggerResponse(400, "Bad request")]
    public async Task<IActionResult> CreateLaboratory(CreateLaboratoryResource resource)
    {
        var command = CreateLaboratoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var laboratory = await laboratoryCommandService.Handle(command);
        if (laboratory is null) return BadRequest();
        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);
        return CreatedAtAction(nameof(GetLaboratoryById), new { id = laboratory.Id }, laboratoryResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update Laboratory")]
    [SwaggerResponse(200, "Laboratory updated", typeof(LaboratoryResource))]
    [SwaggerResponse(404, "Laboratory not found")]
    public async Task<IActionResult> UpdateLaboratory(int id, UpdateLaboratoryResource resource)
    {
        var command = UpdateLaboratoryCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var laboratory = await laboratoryCommandService.Handle(command);
        if (laboratory is null) return NotFound();
        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);
        return Ok(laboratoryResource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete Laboratory")]
    [SwaggerResponse(204, "Laboratory deleted")]
    [SwaggerResponse(404, "Laboratory not found")]
    public async Task<IActionResult> DeleteLaboratory(int id)
    {
        var result = await laboratoryCommandService.DeleteLaboratory(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPost("{id:int}/members")]
    [SwaggerOperation("Add Member")]
    [SwaggerResponse(200, "Member added", typeof(LaboratoryResource))]
    [SwaggerResponse(404, "Laboratory not found")]
    public async Task<IActionResult> AddMember(int id, AddMemberResource resource)
    {
        var command = AddMemberCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var laboratory = await laboratoryCommandService.Handle(command);
        if (laboratory is null) return NotFound();
        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);
        return Ok(laboratoryResource);
    }

    [HttpDelete("{id:int}/members")]
    [SwaggerOperation("Remove Member")]
    [SwaggerResponse(200, "Member removed", typeof(LaboratoryResource))]
    [SwaggerResponse(404, "Laboratory not found")]
    public async Task<IActionResult> RemoveMember(int id, RemoveMemberResource resource)
    {
        var command = RemoveMemberCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var laboratory = await laboratoryCommandService.Handle(command);
        if (laboratory is null) return NotFound();
        var laboratoryResource = LaboratoryResourceFromEntityAssembler.ToResourceFromEntity(laboratory);
        return Ok(laboratoryResource);
    }
}
