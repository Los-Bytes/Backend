namespace Backend.API.Laboratories.Domain.Model.ValueObjects;

/// <summary>
///     Laboratory Name Value Object
/// </summary>
public record LaboratoryName
{
    private const int MaxLength = 200;
    private const int MinLength = 3;

    public string Value { get; }

    public LaboratoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Laboratory name cannot be empty", nameof(value));

        if (value.Length < MinLength)
            throw new ArgumentException($"Laboratory name must be at least {MinLength} characters long", nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentException($"Laboratory name cannot exceed {MaxLength} characters", nameof(value));

        Value = value.Trim();
    }

    public static implicit operator string(LaboratoryName name) => name.Value;
    public static explicit operator LaboratoryName(string value) => new(value);

    public override string ToString() => Value;
}