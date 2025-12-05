namespace Backend.API.Laboratories.Domain.Model.ValueObjects;

/// <summary>
///     Laboratory Capacity Value Object
/// </summary>
public record Capacity
{
    private const int MinCapacity = 1;
    private const int MaxCapacity = 10000;

    public int Value { get; }

    public Capacity(int value)
    {
        if (value < MinCapacity)
            throw new ArgumentException($"Capacity must be at least {MinCapacity}", nameof(value));

        if (value > MaxCapacity)
            throw new ArgumentException($"Capacity cannot exceed {MaxCapacity}", nameof(value));

        Value = value;
    }

    public static implicit operator int(Capacity capacity) => capacity.Value;
    public static explicit operator Capacity(int value) => new(value);

    public bool CanAccommodate(int numberOfPeople) => Value >= numberOfPeople;

    public override string ToString() => Value.ToString();
}