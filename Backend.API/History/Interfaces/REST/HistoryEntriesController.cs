using Backend.API.History.Domain.Model.Commands;
using Backend.API.History.Domain.Model.Queries;
using Backend.API.History.Domain.Services;
using Backend.API.History.Interfaces.REST.Resources;
using Backend.API.History.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.History.Interfaces.REST;

/// <summary>
/// REST controller for managing history entries.
/// </summary>
[ApiController]
[Route("history")]
[Produces("application/json")]
[Authorize]
public class HistoryEntriesController : ControllerBase
{
    private readonly IHistoryEntryCommandService _commandService;
    private readonly IHistoryEntryQueryService _queryService;

    public HistoryEntriesController(
        IHistoryEntryCommandService commandService,
        IHistoryEntryQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    /// <summary>
    /// Lists all history entries.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HistoryEntryResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllHistoryEntriesQuery();
        var entries = await _queryService.Handle(query);
        var resources = HistoryEntryResourceFromEntityAssembler.ToResourceList(entries);
        return Ok(resources);
    }

    /// <summary>
    /// Gets a history entry by its identifier.
    /// </summary>
    [HttpGet("{historyEntryId:int}")]
    [ProducesResponseType(typeof(HistoryEntryResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int historyEntryId)
    {
        var query = new GetHistoryEntryByIdQuery(historyEntryId);
        var entry = await _queryService.Handle(query);

        if (entry is null) return NotFound();

        var resource = HistoryEntryResourceFromEntityAssembler.ToResource(entry);
        return Ok(resource);
    }

    /// <summary>
    /// Creates a new history entry.
    /// </summary>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(HistoryEntryResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateHistoryEntryResource resource)
    {
        var command = CreateHistoryEntryCommandFromResourceAssembler.ToCommand(resource);
        var entry = await _commandService.Handle(command);

        if (entry is null)
            return BadRequest("Unable to create history entry.");

        var createdResource = HistoryEntryResourceFromEntityAssembler.ToResource(entry);
        return CreatedAtAction(nameof(GetById), new { historyEntryId = entry.Id }, createdResource);
    }

    /// <summary>
    /// Deletes a history entry by its identifier.
    /// </summary>
    [HttpDelete("{historyEntryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int historyEntryId)
    {
        var command = new DeleteHistoryEntryCommand(historyEntryId);
        var deleted = await _commandService.Handle(command);

        if (!deleted) return NotFound();

        return NoContent();
    }
}
