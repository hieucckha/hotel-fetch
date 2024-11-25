namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents the images associated with Hotel Patagonia, including rooms and amenities.
/// </summary>
public class HotelPatagoniaImagesDto
{
    /// <summary>
    /// Gets or sets the collection of images for the rooms.
    /// </summary>
    public IReadOnlyCollection<HotelPatagoniaImageDetailDto>? Rooms { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of images for the amenities.
    /// </summary>
    public IReadOnlyCollection<HotelPatagoniaImageDetailDto>? Amenities { get; set; } = [];
}
