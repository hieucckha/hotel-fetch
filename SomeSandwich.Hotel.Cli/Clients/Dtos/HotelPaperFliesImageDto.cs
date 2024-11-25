namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents an image of a hotel.
/// </summary>
public class HotelPaperFliesImageDto
{
    /// <summary>
    /// Gets or sets the link to the image.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the caption for the image.
    /// </summary>
    public string? Caption { get; set; }
}
