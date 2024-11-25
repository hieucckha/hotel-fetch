namespace SomeSandwich.Hotel.Cli.Models;

/// <summary>
/// Represents the location details of a hotel.
/// </summary>
public class HotelLocation
{
    /// <summary>
    /// Gets or sets the latitude of the hotel location.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the hotel location.
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
}
