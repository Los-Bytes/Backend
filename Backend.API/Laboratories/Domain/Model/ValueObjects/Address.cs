namespace Backend.API.Laboratories.Domain.Model.ValueObjects;

/// <summary>
///     Address Value Object
/// </summary>
public record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }

    public Address(string street, string city, string state, string zipCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty", nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty", nameof(city));

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));

        Street = street.Trim();
        City = city.Trim();
        State = state?.Trim() ?? string.Empty;
        ZipCode = zipCode?.Trim() ?? string.Empty;
        Country = country.Trim();
    }

    public static Address FromFullAddress(string fullAddress)
    {
        if (string.IsNullOrWhiteSpace(fullAddress))
            throw new ArgumentException("Address cannot be empty", nameof(fullAddress));

        var parts = fullAddress.Split(',');
        return new Address(
            street: parts.Length > 0 ? parts[0].Trim() : fullAddress,
            city: parts.Length > 1 ? parts[1].Trim() : "Unknown",
            state: parts.Length > 2 ? parts[2].Trim() : "",
            zipCode: parts.Length > 3 ? parts[3].Trim() : "",
            country: parts.Length > 4 ? parts[4].Trim() : "Unknown"
        );
    }

    public string ToFullAddressString()
    {
        var parts = new List<string> { Street, City };
        if (!string.IsNullOrWhiteSpace(State)) parts.Add(State);
        if (!string.IsNullOrWhiteSpace(ZipCode)) parts.Add(ZipCode);
        parts.Add(Country);
        return string.Join(", ", parts);
    }

    public override string ToString() => ToFullAddressString();
}
