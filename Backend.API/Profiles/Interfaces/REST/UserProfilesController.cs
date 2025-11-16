using System.Net.Mime;
using Backend.API.Profiles.Domain.Model.Queries;
using Backend.API.Profiles.Domain.Services;
using Backend.API.Profiles.Interfaces.REST.Resources;
using Backend.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Profiles.Interfaces.REST;

/// <summary>
///     Controller for managing user profiles.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Profile Endpoints.")]
public class UserProfilesController(
    IUserProfileCommandService userProfileCommandService,
    IUserProfileQueryService userProfileQueryService)
    : ControllerBase
{
    /// <summary>
    ///     Get a user profile by its unique identifier
    /// </summary>
    [HttpGet("{userId:int}")]
    [SwaggerOperation("Get User Profile by Id", "Get a user profile by its unique identifier.", 
        OperationId = "GetUserProfileById")]
    [SwaggerResponse(200, "The user profile was found and returned.", typeof(UserProfileResource))]
    [SwaggerResponse(404, "The user profile was not found.")]
    public async Task<IActionResult> GetUserProfileById(int userId)
    {
        var getUserProfileByIdQuery = new GetUserProfileByIdQuery(userId);
        var userProfile = await userProfileQueryService.Handle(getUserProfileByIdQuery);
        if (userProfile is null) return NotFound();
        var userProfileResource = UserProfileResourceFromEntityAssembler.ToResourceFromEntity(userProfile);
        return Ok(userProfileResource);
    }

    /// <summary>
    ///     Create a new user profile
    /// </summary>
    [HttpPost]
    [SwaggerOperation("Create User Profile", "Create a new user profile.", OperationId = "CreateUserProfile")]
    [SwaggerResponse(201, "The user profile was created.", typeof(UserProfileResource))]
    [SwaggerResponse(400, "The user profile was not created.")]
    public async Task<IActionResult> CreateUserProfile(CreateUserProfileResource resource)
    {
        var createUserProfileCommand = 
            CreateUserProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var userProfile = await userProfileCommandService.Handle(createUserProfileCommand);
        if (userProfile is null) return BadRequest();
        var userProfileResource = UserProfileResourceFromEntityAssembler.ToResourceFromEntity(userProfile);
        return CreatedAtAction(nameof(GetUserProfileById), new { userId = userProfile.Id }, 
            userProfileResource);
    }

    /// <summary>
    ///     Get all user profiles
    /// </summary>
    [HttpGet]
    [SwaggerOperation("Get All User Profiles", "Get all user profiles.", OperationId = "GetAllUserProfiles")]
    [SwaggerResponse(200, "The user profiles were found and returned.", 
        typeof(IEnumerable<UserProfileResource>))]
    [SwaggerResponse(404, "The user profiles were not found.")]
    public async Task<IActionResult> GetAllUserProfiles()
    {
        var getAllUserProfilesQuery = new GetAllUserProfilesQuery();
        var userProfiles = await userProfileQueryService.Handle(getAllUserProfilesQuery);
        var userProfileResources = userProfiles.Select(
            UserProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userProfileResources);
    }

    /// <summary>
    ///     Update user notification preferences
    /// </summary>
    [HttpPatch("{userId:int}/preferences")]
    [SwaggerOperation("Update User Preferences", 
        "Update user notification preferences.", OperationId = "UpdateUserPreferences")]
    [SwaggerResponse(200, "The user preferences were updated.", typeof(UserProfileResource))]
    [SwaggerResponse(404, "The user profile was not found.")]
    [SwaggerResponse(400, "The user preferences were not updated.")]
    public async Task<IActionResult> UpdateUserPreferences(int userId, 
        UpdateUserPreferencesResource resource)
    {
        var updateUserPreferencesCommand = 
            UpdateUserPreferencesCommandFromResourceAssembler.ToCommandFromResource(userId, resource);
        var userProfile = await userProfileCommandService.Handle(updateUserPreferencesCommand);
        if (userProfile is null) return NotFound();
        var userProfileResource = UserProfileResourceFromEntityAssembler.ToResourceFromEntity(userProfile);
        return Ok(userProfileResource);
    }
}
