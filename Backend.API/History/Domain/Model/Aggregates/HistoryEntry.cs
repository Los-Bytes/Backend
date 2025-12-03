namespace Backend.API.History.Domain.Model.Aggregates
{
    /// <summary>
    /// History entry aggregate root.
    /// </summary>
    public class HistoryEntry
    {
        public int Id { get; private set; }

        /// <summary>
        /// Inventory item identifier related to this history entry.
        /// </summary>
        public int? InventoryItemId { get; private set; }

        /// <summary>
        /// Laboratory identifier related to this history entry.
        /// </summary>
        public int? LaboratoryId { get; private set; }

        /// <summary>
        /// Action performed (created, updated, sold, used, returned, etc.).
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// Previous status of the inventory item.
        /// </summary>
        public string PreviousStatus { get; private set; }

        /// <summary>
        /// New status of the inventory item.
        /// </summary>
        public string NewStatus { get; private set; }

        /// <summary>
        /// Quantity affected by this action.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// User identifier that performed the action.
        /// </summary>
        public int? UserId { get; private set; }

        /// <summary>
        /// User name that performed the action.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Timestamp of when the action happened.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Description of the action.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Creation date of the entry (filled automatically by interceptor).
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Last update date of the entry (filled automatically by interceptor).
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// EF Core parameterless constructor.
        /// </summary>
        protected HistoryEntry()
        {
            Action = string.Empty;
            PreviousStatus = string.Empty;
            NewStatus = string.Empty;
            UserName = string.Empty;
            Description = string.Empty;
        }

        public HistoryEntry(
            int? inventoryItemId,
            int? laboratoryId,
            string action,
            string previousStatus,
            string newStatus,
            int quantity,
            int? userId,
            string userName,
            DateTime timestamp,
            string description)
        {
            InventoryItemId = inventoryItemId;
            LaboratoryId = laboratoryId;
            Action = action;
            PreviousStatus = previousStatus;
            NewStatus = newStatus;
            Quantity = quantity;
            UserId = userId;
            UserName = userName;
            Timestamp = timestamp;
            Description = description;
        }

        public void Update(
            int? inventoryItemId,
            int? laboratoryId,
            string action,
            string previousStatus,
            string newStatus,
            int quantity,
            int? userId,
            string userName,
            DateTime timestamp,
            string description)
        {
            InventoryItemId = inventoryItemId;
            LaboratoryId = laboratoryId;
            Action = action;
            PreviousStatus = previousStatus;
            NewStatus = newStatus;
            Quantity = quantity;
            UserId = userId;
            UserName = userName;
            Timestamp = timestamp;
            Description = description;
        }
    }
}
