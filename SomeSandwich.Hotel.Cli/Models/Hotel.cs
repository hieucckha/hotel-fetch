using Newtonsoft.Json;

namespace SomeSandwich.Hotel.Cli.Models;

/// <summary>
/// Represents a hotel with various properties such as location, amenities, images, and booking conditions.
/// </summary>
public class Hotel
{
    /// <summary>
    /// Gets or sets the unique identifier for the hotel.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination identifier for the hotel.
    /// </summary>
    public int DestinationId { get; set; }

    /// <summary>
    /// Gets or sets the name of the hotel.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the location details of the hotel.
    /// </summary>
    public HotelLocation Location { get; set; } = new();

    /// <summary>
    /// Gets or sets the description of the hotel.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the amenities available at the hotel.
    /// </summary>
    public HotelAmenities Amenities { get; set; } = new();

    /// <summary>
    /// Gets or sets the images associated with the hotel.
    /// </summary>
    public HotelImages Images { get; set; } = new();

    /// <summary>
    /// Gets or sets the booking conditions for the hotel.
    /// </summary>
    public IReadOnlyCollection<string> BookingConditions { get; set; } = [];
}
