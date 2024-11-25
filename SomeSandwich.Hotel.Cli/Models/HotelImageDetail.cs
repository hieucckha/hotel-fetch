namespace SomeSandwich.Hotel.Cli.Models;

/// <summary>
/// Represents the details of a hotel image, including its link and description.
/// </summary>
public class HotelImageDetail
{
    /// <summary>
    /// Gets or sets the link to the hotel image.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the description of the hotel image.
    /// </summary>
    public string? Description { get; set; }
}

/// <inheritdoc />
public class HotelImageDetailComparer : IEqualityComparer<HotelImageDetail>
{
    /// <inheritdoc />
    public bool Equals(HotelImageDetail? x, HotelImageDetail? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Link == y.Link;
    }

    /// <inheritdoc />
    public int GetHashCode(HotelImageDetail obj)
    {
        return obj.Link.GetHashCode();
    }
}
