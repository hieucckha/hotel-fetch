namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents the images of a hotel.
/// </summary>
public class HotelPaperFliesImagesDto
{
    /// <summary>
    /// Gets or sets the images of the hotel rooms.
    /// </summary>
    public IReadOnlyCollection<HotelPaperFliesImageDto>? Rooms { get; set; } = [];

    /// <summary>
    /// Gets or sets the images of the hotel site.
    /// </summary>
    public IReadOnlyCollection<HotelPaperFliesImageDto>? Site { get; set; } = [];
}
