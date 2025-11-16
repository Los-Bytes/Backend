using Backend.API.Profiles.Domain.Model.Commands;
using Backend.API.Profiles.Interfaces.REST.Resources;

namespace Backend.API.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Assembler to create a CreateUserProfileCommand command from a resource
/// </summary>
public static class CreateUserProfileCommandFromResourceAssembler
{
    /// <summary>
    ///     Create a CreateUserProfileCommand command from a resource
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateUserProfileResource" /> resource to create the command from
    /// </param>
    /// <returns>
    ///     The <see cref="CreateUserProfileCommand" /> command created from the resource
    /// </returns>
    public static CreateUserProfileCommand ToCommandFromResource(CreateUserProfileResource resource)
    {
        return new CreateUserProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email
        );
    }
}