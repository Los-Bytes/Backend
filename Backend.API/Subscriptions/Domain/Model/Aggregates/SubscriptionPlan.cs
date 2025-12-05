using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.Subscriptions.Domain.Model.Aggregates;

public class SubscriptionPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(10)]
    public string Currency { get; set; } = "USD";
    
    [Required]
    [MaxLength(20)]
    public string Period { get; set; } = "monthly";
    
    [Required]
    public int MaxMembers { get; set; }
    
    [Required]
    public int MaxInventoryItems { get; set; }
    
    [Required]
    [Column(TypeName = "json")]
    public string Features { get; set; } = "[]";

    public bool IsFree()
    {
        return Price == 0;
    }

    public bool IsUnlimited()
    {
        return MaxMembers == -1 && MaxInventoryItems == -1;
    }

    public string GetPriceFormatted()
    {
        return IsFree() ? "Gratis" : $"${Price:F2} {Currency}";
    }

    public List<string> GetFeaturesList()
    {
        return System.Text.Json.JsonSerializer.Deserialize<List<string>>(Features) ?? new List<string>();
    }

    public void SetFeaturesList(List<string> features)
    {
        Features = System.Text.Json.JsonSerializer.Serialize(features);
    }
}