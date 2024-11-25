using Newtonsoft.Json;

namespace SomeSandwich.Hotel.Cli.Clients.Dtos;

/// <summary>
/// Represents the details of a hotel including its location, amenities, images, and booking conditions.
/// </summary>
public class HotelPaperFliesDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the hotel.
    /// </summary>
    [JsonProperty(PropertyName = "hotel_id")]
    public string HotelId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier for the destination where the hotel is located.
    /// </summary>
    [JsonProperty(PropertyName = "destination_id")]
    public int DestinationId { get; set; }

    /// <summary>
    /// Gets or sets the name of the hotel.
    /// </summary>
    [JsonProperty(PropertyName = "hotel_name")]
    public string? HotelName { get; set; }

    /// <summary>
    /// Gets or sets the location details of the hotel.
    /// </summary>
    public HotelPaperFliesLocationDto? Location { get; set; } = new();

    /// <summary>
    /// Gets or sets additional details about the hotel.
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets the amenities available at the hotel.
    /// </summary>
    public HotelPaperFliesAmenitiesDto? Amenities { get; set; } = new();

    /// <summary>
    /// Gets or sets the images of the hotel.
    /// </summary>
    public HotelPaperFliesImagesDto? Images { get; set; } = new();

    /// <summary>
    /// Gets or sets the booking conditions for the hotel.
    /// </summary>
    [JsonProperty(PropertyName = "booking_conditions")]
    public IReadOnlyCollection<string>? BookingConditions { get; set; } = [];
}
