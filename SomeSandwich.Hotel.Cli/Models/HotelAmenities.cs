namespace SomeSandwich.Hotel.Cli.Models;

/// <summary>
/// Represents the amenities available in a hotel.
/// </summary>
public class HotelAmenities
{
    /// <summary>
    /// Gets or sets the general amenities available in the hotel.
    /// </summary>
    public IReadOnlyCollection<string> General { get; set; } = [];

    /// <summary>
    /// Gets or sets the amenities available in the hotel rooms.
    /// </summary>
    public IReadOnlyCollection<string> Room { get; set; } = [];
}
