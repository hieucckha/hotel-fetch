namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents the amenities available at a hotel.
/// </summary>
public class HotelPaperFliesAmenitiesDto
{
    /// <summary>
    /// Gets or sets the general amenities available at the hotel.
    /// </summary>
    public ICollection<string>? General { get; set; } = [];

    /// <summary>
    /// Gets or sets the room-specific amenities available at the hotel.
    /// </summary>
    public ICollection<string>? Room { get; set; } = [];
}
