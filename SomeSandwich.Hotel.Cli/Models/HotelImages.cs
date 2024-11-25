namespace SomeSandwich.Hotel.Cli.Models;

/// <summary>
/// Represents the images associated with a hotel, categorized by rooms, site, and amenities.
/// </summary>
public class HotelImages
{
    /// <summary>
    /// Gets or sets the collection of images for the hotel rooms.
    /// </summary>
    public IReadOnlyCollection<HotelImageDetail> Rooms { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of images for the hotel site.
    /// </summary>
    public IReadOnlyCollection<HotelImageDetail> Site { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of images for the hotel amenities.
    /// </summary>
    public IReadOnlyCollection<HotelImageDetail> Amenities { get; set; } = [];
}
