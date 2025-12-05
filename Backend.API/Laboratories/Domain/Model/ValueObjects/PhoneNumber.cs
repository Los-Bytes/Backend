using System.Text.RegularExpressions;

namespace Backend.API.Laboratories.Domain.Model.ValueObjects;

/// <summary>
///     Phone Number Value Object
/// </summary>
public record PhoneNumber
{
    private static readonly Regex PhoneRegex = new(@"^\+?[\d\s\-\(\)]+$", RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty", nameof(value));

        var cleanValue = value.Trim();

        if (cleanValue.Length < 7 || cleanValue.Length > 20)
            throw new ArgumentException("Phone number must be between 7 and 20 characters", nameof(value));

        if (!PhoneRegex.IsMatch(cleanValue))
            throw new ArgumentException("Phone number format is invalid", nameof(value));

        Value = cleanValue;
    }

    public static implicit operator string(PhoneNumber phone) => phone.Value;
    public static explicit operator PhoneNumber(string value) => new(value);

    public override string ToString() => Value;
}