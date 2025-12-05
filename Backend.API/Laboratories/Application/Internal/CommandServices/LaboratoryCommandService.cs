using Backend.API.Laboratories.Domain.Model.Aggregates;
using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Domain.Model.Queries;
using Backend.API.Laboratories.Domain.Repositories;
using Backend.API.Laboratories.Domain.Services;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.Laboratories.Application.Internal.CommandServices;

/// <summary>
///     Laboratory command service implementation
/// </summary>
public class LaboratoryCommandService(
    ILaboratoryRepository laboratoryRepository,
    ILaboratoryQueryService laboratoryQueryService,
    IUnitOfWork unitOfWork
) : ILaboratoryCommandService
{
    public async Task<Laboratory?> Handle(CreateLaboratoryCommand command)
    {
        try
        {
            var laboratory = new Laboratory(command);
            await laboratoryRepository.AddAsync(laboratory);
            await unitOfWork.CompleteAsync();
            return laboratory;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Laboratory?> Handle(UpdateLaboratoryCommand command)
    {
        try
        {
            var query = new GetLaboratoryByIdQuery(command.Id);
            var laboratory = await laboratoryQueryService.Handle(query);

            if (laboratory == null) return null;

            laboratory.Update(command.Name, command.Address, command.Phone, command.Capacity);
            
            laboratoryRepository.Update(laboratory);
            await unitOfWork.CompleteAsync();
            return laboratory;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Laboratory?> Handle(AddMemberCommand command)
    {
        try
        {
            var query = new GetLaboratoryByIdQuery(command.LaboratoryId);
            var laboratory = await laboratoryQueryService.Handle(query);

            if (laboratory == null) return null;

            laboratory.AddMember(command.UserId);
            
            laboratoryRepository.Update(laboratory);
            await unitOfWork.CompleteAsync();
            return laboratory;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Laboratory?> Handle(RemoveMemberCommand command)
    {
        try
        {
            var query = new GetLaboratoryByIdQuery(command.LaboratoryId);
            var laboratory = await laboratoryQueryService.Handle(query);

            if (laboratory == null) return null;

            laboratory.RemoveMember(command.UserId);
            
            laboratoryRepository.Update(laboratory);
            await unitOfWork.CompleteAsync();
            return laboratory;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> DeleteLaboratory(int id)
    {
        try
        {
            var query = new GetLaboratoryByIdQuery(id);
            var laboratory = await laboratoryQueryService.Handle(query);

            if (laboratory == null) return false;

            laboratoryRepository.Remove(laboratory);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
