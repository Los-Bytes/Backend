using Backend.API.History.Domain.Model.Aggregates;
using Backend.API.History.Domain.Model.Commands;
using Backend.API.History.Domain.Repositories;
using Backend.API.History.Domain.Services;
using Backend.API.Shared.Domain.Repositories;

namespace Backend.API.History.Application.Internal.CommandServices
{
    /// <summary>
    /// Command service implementation for history entries.
    /// </summary>
    public class HistoryEntryCommandService : IHistoryEntryCommandService
    {
        private readonly IHistoryEntryRepository _historyEntryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HistoryEntryCommandService(
            IHistoryEntryRepository historyEntryRepository,
            IUnitOfWork unitOfWork)
        {
            _historyEntryRepository = historyEntryRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<HistoryEntry?> Handle(CreateHistoryEntryCommand command)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(command.Action))
                    return null;

                var timestamp = command.Timestamp == default
                    ? DateTime.UtcNow
                    : command.Timestamp;

                var entry = new HistoryEntry(
                    command.InventoryItemId,
                    command.LaboratoryId,
                    command.Action,
                    command.PreviousStatus,
                    command.NewStatus,
                    command.Quantity,
                    command.UserId,
                    command.UserName,
                    timestamp,
                    command.Description);

                await _historyEntryRepository.AddAsync(entry);
                await _unitOfWork.CompleteAsync();
                return entry;
            }
            catch
            {
                // Following Learning Center style: return null on error.
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<bool> Handle(DeleteHistoryEntryCommand command)
        {
            try
            {
                var entry = await _historyEntryRepository.FindByIdAsync(command.HistoryEntryId);
                if (entry is null) return false;

                _historyEntryRepository.Remove(entry);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
