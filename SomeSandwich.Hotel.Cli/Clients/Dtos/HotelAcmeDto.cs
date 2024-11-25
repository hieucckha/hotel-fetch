namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents a Data Transfer Object (DTO) for Hotel Acme.
/// </summary>
public class HotelAcmeDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the hotel.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination identifier where the hotel is located.
    /// </summary>
    public int DestinationId { get; set; }

    /// <summary>
    /// Gets or sets the name of the hotel.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the hotel's location.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the hotel's location.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the address of the hotel.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the city where the hotel is located.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the country where the hotel is located.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the hotel's location.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the description of the hotel.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the list of facilities available at the hotel.
    /// </summary>
    public IReadOnlyList<string>? Facilities { get; set; } = [];
}
