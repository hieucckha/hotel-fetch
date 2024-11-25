using SomeSandwich.Hotel.Cli.Clients;
using SomeSandwich.Hotel.Cli.Clients.Dtos;
using SomeSandwich.Hotel.Cli.Extensions;
using SomeSandwich.Hotel.Cli.Models;

namespace SomeSandwich.Hotel.Cli.Services;

/// <summary>
/// Service interface for merging hotel data from different sources.
/// </summary>
public interface IHotelMergerService
{
    /// <summary>
    /// Searches and merges hotel data from different sources based on provided hotel and destination IDs.
    /// </summary>
    /// <param name="hotelIds">A collection of hotel IDs to search for.</param>
    /// <param name="destinationIds">A collection of destination IDs to search for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only collection of merged hotel data.</returns>
    Task<IReadOnlyCollection<Models.Hotel>> SearchMergeHotelAsync(IEnumerable<string> hotelIds, IEnumerable<int> destinationIds);
}

/// <inheritdoc />
public class HotelMergerService : IHotelMergerService
{
    private readonly IHotelClient hotelClient;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="hotelClient">Hotel client instance.</param>
    public HotelMergerService(IHotelClient hotelClient)
    {
        this.hotelClient = hotelClient;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Models.Hotel>> SearchMergeHotelAsync(IEnumerable<string> hotelIds, IEnumerable<int> destinationIds)
    {

        var acmeTask = hotelClient.SearchHotelFromAcmeAsync();
        var patagoniaTask = hotelClient.SearchHotelFromPatagoniaAsync();
        var paperFliesTask = hotelClient.SearchHotelFromPaperFliesAsync();

        await Task.WhenAll(acmeTask, patagoniaTask, paperFliesTask);
        var acmeHotelDtos = acmeTask.Result.Data ?? [];
        var patagoniaDtos = patagoniaTask.Result.Data ?? [];
        var paperFliesDtos = paperFliesTask.Result.Data ?? [];

        var hotels = new Dictionary<(string Id, int DestinationId), Models.Hotel>();

        MapPaperFliesToHotel(hotels, paperFliesDtos);
        MapPatagoniaHotelToHotel(hotels, patagoniaDtos);
        MapAcmeHotelToHotel(hotels, acmeHotelDtos);

        return hotels.Values.ToList();
    }

    private static void MapPaperFliesToHotel(IDictionary<(string Id, int DestinationId), Models.Hotel> hotels, IEnumerable<HotelPaperFliesDto> paperFliesDtos)
    {
        foreach (var paperFliesHotel in paperFliesDtos)
        {
            if (hotels.TryGetValue((paperFliesHotel.HotelId, paperFliesHotel.DestinationId), out var hotel))
            {
                hotel.Name = string.IsNullOrEmpty(hotel.Name) && !string.IsNullOrEmpty(paperFliesHotel.HotelName) ? paperFliesHotel.HotelName : hotel.Name;
                hotel.Location.Address = string.IsNullOrEmpty(hotel.Location.Address) && !string.IsNullOrEmpty(paperFliesHotel?.Location?.Address) ? paperFliesHotel.Location.Address : hotel.Location.Address;
                hotel.Location.Country = !string.IsNullOrEmpty(paperFliesHotel?.Location?.Country) ? paperFliesHotel.Location.Country : hotel.Location.Country;
                hotel.Description = !string.IsNullOrEmpty(paperFliesHotel?.Details) ? paperFliesHotel.Details.BeautifyNullable() : hotel.Description;
                hotel.Amenities.General = hotel.Amenities.General
                    .Union(paperFliesHotel?.Amenities?.General?.Select(f => f.BeautifyAndToLower()) ?? [])
                    .ToList();
                hotel.Amenities.Room = hotel.Amenities.Room
                    .Union(paperFliesHotel?.Amenities?.Room?.Select(f => f.BeautifyAndToLower()) ?? [])
                    .ToList();
                hotel.Images.Rooms = hotel.Images.Rooms
                    .Union(paperFliesHotel?.Images?.Rooms?.Select(i => new HotelImageDetail
                    {
                        Link = i.Link,
                        Description = i.Caption
                    }) ?? [], new HotelImageDetailComparer())
                    .ToList();
                hotel.Images.Site = hotel.Images.Site
                    .Union(paperFliesHotel?.Images?.Site?.Select(i => new HotelImageDetail
                    {
                        Link = i.Link,
                        Description = i.Caption
                    }) ?? [], new HotelImageDetailComparer())
                    .ToList();
                hotel.BookingConditions = hotel.BookingConditions
                    .Union(paperFliesHotel?.BookingConditions?.Select(c => c.Beautify()) ?? [])
                    .ToList();

                hotels[(paperFliesHotel!.HotelId, paperFliesHotel.DestinationId)] = hotel;
            }
            else
            {
                var newHotel = new Models.Hotel
                {
                    Id = paperFliesHotel.HotelId,
                    DestinationId = paperFliesHotel.DestinationId,
                    Name = paperFliesHotel.HotelName.BeautifyNullable(),
                    Location = new HotelLocation
                    {
                        Address = paperFliesHotel?.Location?.Address.BeautifyNullable(),
                        Country = paperFliesHotel?.Location?.Country.BeautifyNullable(),
                    },
                    Description = paperFliesHotel?.Details.BeautifyNullable(),
                    Amenities = new HotelAmenities
                    {
                        General = paperFliesHotel?.Amenities?.General?.Select(e => e.Beautify()).ToList() ?? [],
                        Room = paperFliesHotel?.Amenities?.Room?.Select(e => e.Beautify()).ToList() ?? []
                    },
                    Images = new HotelImages
                    {
                        Rooms = paperFliesHotel?.Images?.Rooms?.Select(i => new HotelImageDetail
                        {
                            Link = i.Link,
                            Description = i.Caption
                        }).ToList() ?? [],
                        Site = paperFliesHotel?.Images?.Site?.Select(i => new HotelImageDetail
                        {
                            Link = i.Link,
                            Description = i.Caption
                        }).ToList() ?? []
                    },
                    BookingConditions = paperFliesHotel?.BookingConditions?.Select(e => e.Beautify()).ToList() ?? []
                };

                hotels.Add((paperFliesHotel!.HotelId, paperFliesHotel.DestinationId), newHotel);
            }
        }
    }

    private static void MapPatagoniaHotelToHotel(IDictionary<(string Id, int DestinationId), Models.Hotel> hotels, IEnumerable<HotelPatagoniaDto> patagoniaDtos)
    {
        foreach (var patagoniaHotel in patagoniaDtos)
        {
            if (hotels.TryGetValue((patagoniaHotel.Id, patagoniaHotel.Destination), out var hotel))
            {
                hotel.Name = string.IsNullOrEmpty(hotel.Name) && !string.IsNullOrEmpty(patagoniaHotel.Name) ? patagoniaHotel.Name : hotel.Name;
                hotel.Location.Latitude = hotel.Location.Latitude.IsNullOrZero() && !patagoniaHotel.Latitude.IsNullOrZero() ? patagoniaHotel.Latitude : hotel.Location.Latitude;
                hotel.Location.Longitude = hotel.Location.Longitude.IsNullOrZero() && !patagoniaHotel.Longitude.IsNullOrZero() ? patagoniaHotel.Latitude : hotel.Location.Longitude;
                hotel.Location.Address = string.IsNullOrEmpty(hotel.Location.Address) && !string.IsNullOrEmpty(patagoniaHotel.Address) ? patagoniaHotel.Address : hotel.Location.Address;
                hotel.Description = string.IsNullOrEmpty(hotel.Description) && !string.IsNullOrEmpty(patagoniaHotel.Information) ? patagoniaHotel.Information : hotel.Description;
                hotel.Amenities.Room = hotel.Amenities.Room
                    .Union(patagoniaHotel?.Amenities?.Select(f => f.BeautifyAndToLower()) ?? [])
                    .ToList();
                hotel.Images.Rooms = hotel.Images.Rooms
                    .Union(patagoniaHotel?.Images?.Rooms?.Select(i => new HotelImageDetail
                    {
                        Link = i.Url,
                        Description = i.Description
                    }) ?? [], new HotelImageDetailComparer())
                    .ToList();
                hotel.Images.Amenities = hotel.Images.Amenities
                    .Union(patagoniaHotel?.Images?.Amenities?.Select(i => new HotelImageDetail
                    {
                        Link = i.Url,
                        Description = i.Description
                    }) ?? [], new HotelImageDetailComparer())
                    .ToList();

                hotels[(patagoniaHotel!.Id, patagoniaHotel.Destination)] = hotel;
            }
            else
            {
                var newHotel = new Models.Hotel
                {
                    Id = patagoniaHotel.Id,
                    DestinationId = patagoniaHotel.Destination,
                    Name = patagoniaHotel.Name.BeautifyNullable(),
                    Location = new HotelLocation
                    {
                        Latitude = patagoniaHotel.Latitude,
                        Longitude = patagoniaHotel.Longitude,
                        Address = patagoniaHotel.Address.BeautifyNullable(),
                    },
                    Description = patagoniaHotel.Information.BeautifyNullable(),
                    Amenities = new HotelAmenities
                    {
                        Room = patagoniaHotel.Amenities?.Select(a => a.Beautify()).ToList() ?? []
                    },
                    Images = new HotelImages
                    {
                        Rooms = patagoniaHotel?.Images?.Rooms?.Select(i => new HotelImageDetail
                        {
                            Link = i.Url,
                            Description = i.Description
                        }).ToList() ?? [],
                        Amenities = patagoniaHotel?.Images?.Rooms?.Select(i => new HotelImageDetail
                        {
                            Link = i.Url,
                            Description = i.Description
                        }).ToList() ?? [],
                    }
                };

                hotels.Add((patagoniaHotel!.Id, patagoniaHotel.Destination), newHotel);
            }
        }
    }

    private static void MapAcmeHotelToHotel(IDictionary<(string Id, int DestinationId), Models.Hotel> hotels, IEnumerable<HotelAcmeDto> acmeHotelDtos)
    {
        foreach (var acmeHotel in acmeHotelDtos)
        {
            if (hotels.TryGetValue((acmeHotel.Id, acmeHotel.DestinationId), out var hotel))
            {
                hotel.Name = string.IsNullOrEmpty(hotel.Name) && !string.IsNullOrEmpty(acmeHotel.Name) ? acmeHotel.Name : hotel.Name;
                hotel.Location.Latitude = hotel.Location.Latitude.IsNullOrZero() && acmeHotel.Latitude.IsNullOrZero() ? acmeHotel.Latitude : hotel.Location.Latitude;
                hotel.Location.Longitude = hotel.Location.Longitude.IsNullOrZero() && acmeHotel.Longitude.IsNullOrZero() ? acmeHotel.Latitude : hotel.Location.Longitude;
                hotel.Location.Address = string.IsNullOrEmpty(hotel.Location.Address) && !string.IsNullOrEmpty(acmeHotel.Address) ? acmeHotel.Address : hotel.Location.Address;
                hotel.Location.City = string.IsNullOrEmpty(hotel.Location.City) && !string.IsNullOrEmpty(acmeHotel.City) ? acmeHotel.City : hotel.Location.City;
                hotel.Location.Country = string.IsNullOrEmpty(hotel.Location.Country) && !string.IsNullOrEmpty(acmeHotel.Country) ? acmeHotel.Country : hotel.Location.Country;
                hotel.Description = string.IsNullOrEmpty(hotel.Description) && !string.IsNullOrEmpty(acmeHotel.Description) ? acmeHotel.Description : hotel.Description;
                hotel.Amenities.General = hotel.Amenities.General
                    .Union(acmeHotel?.Facilities?.Select(f => f.BeautifyAndToLower()) ?? [])
                    .ToList();

                hotels[(acmeHotel!.Id, acmeHotel.DestinationId)] = hotel;
            }
            else
            {
                var newHotel = new Models.Hotel
                {
                    Id = acmeHotel.Id,
                    DestinationId = acmeHotel.DestinationId,
                    Name = acmeHotel.Name.BeautifyNullable(),
                    Location = new HotelLocation
                    {
                        Latitude = acmeHotel.Latitude,
                        Longitude = acmeHotel.Longitude,
                        Address = acmeHotel.Address.BeautifyNullable(),
                        City = acmeHotel.City.BeautifyNullable(),
                        Country = acmeHotel.Country.BeautifyNullable()
                    },
                    Description = acmeHotel.Description.BeautifyNullable(),
                    Amenities = new HotelAmenities
                    {
                        General = acmeHotel?.Facilities?.Select(e => e.BeatifyAndSplitAndToLower()).ToList() ?? []
                    }
                };

                hotels.Add((acmeHotel!.Id, acmeHotel.DestinationId), newHotel);
            }
        }
    }
}
