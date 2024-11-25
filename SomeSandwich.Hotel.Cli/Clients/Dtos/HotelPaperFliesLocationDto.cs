namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents the location details of a hotel.
/// </summary>
public class HotelPaperFliesLocationDto
{
    /// <summary>
    /// Gets or sets the address of the hotel.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the country where the hotel is located.
    /// </summary>
    public string? Country { get; set; }
}
