using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.Subscriptions.Domain.Model.Aggregates;

public class Subscription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string PlanType { get; set; } = "Free";
    
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [Required]
    public int MaxMembers { get; set; } = 3;
    
    [Required]
    public int MaxInventoryItems { get; set; } = 50;
    
    [Required]
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    protected Subscription() { }

    public static Subscription Create(int userId, string planType, DateTime startDate, 
        int maxMembers, int maxInventoryItems)
    {
        return new Subscription
        {
            UserId = userId,
            PlanType = planType,
            StartDate = startDate,
            MaxMembers = maxMembers,
            MaxInventoryItems = maxInventoryItems,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Deactivate()
    {
        IsActive = false;
        EndDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsExpired()
    {
        return EndDate.HasValue && EndDate.Value < DateTime.UtcNow;
    }

    public bool HasUnlimitedMembers()
    {
        return MaxMembers == -1;
    }

    public bool HasUnlimitedItems()
    {
        return MaxInventoryItems == -1;
    }

    public void Update(int maxMembers, int maxInventoryItems, bool isActive)
    {
        MaxMembers = maxMembers;
        MaxInventoryItems = maxInventoryItems;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
