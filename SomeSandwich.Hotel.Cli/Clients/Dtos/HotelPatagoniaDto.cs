using Newtonsoft.Json;

namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents the data transfer object for Hotel Patagonia.
/// </summary>
public class HotelPatagoniaDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the hotel.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination identifier for the hotel.
    /// </summary>
    public int Destination { get; set; }

    /// <summary>
    /// Gets or sets the name of the hotel.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the hotel's location.
    /// </summary>
    [JsonProperty(PropertyName = "lat")]
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the hotel's location.
    /// </summary>
    [JsonProperty(PropertyName = "lng")]
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the address of the hotel.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets additional information about the hotel.
    /// </summary>
    [JsonProperty(PropertyName = "info")]
    public string? Information { get; set; }

    /// <summary>
    /// Gets or sets the list of amenities available at the hotel.
    /// </summary>
    public IReadOnlyCollection<string>? Amenities { get; set; } = [];

    /// <summary>
    /// Gets or sets the images associated with the hotel.
    /// </summary>
    public HotelPatagoniaImagesDto? Images { get; set; }
}
