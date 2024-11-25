using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using SomeSandwich.Hotel.Cli.Clients.Dtos;

namespace SomeSandwich.Hotel.Cli.Clients;

/// <summary>
/// Interface for HotelClient which provides methods to search hotels from different suppliers.
/// </summary>
public interface IHotelClient : IDisposable
{
    /// <summary>
    /// Searches for hotels from Acme supplier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with a read-only collection of HotelAcmeDto.</returns>
    Task<RestResponse<IReadOnlyCollection<HotelAcmeDto>>> SearchHotelFromAcmeAsync();

    /// <summary>
    /// Searches for hotels from Patagonia supplier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with a read-only collection of HotelPatagoniaDto.</returns>
    Task<RestResponse<IReadOnlyCollection<HotelPatagoniaDto>>> SearchHotelFromPatagoniaAsync();

    /// <summary>
    /// Searches for hotels from PaperFlies supplier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with a read-only collection of HotelPaperFliesDto.</returns>
    Task<RestResponse<IReadOnlyCollection<HotelPaperFliesDto>>> SearchHotelFromPaperFliesAsync();
}

/// <summary>
/// Client for interacting with hotel suppliers.
/// </summary>
public sealed class HotelClient : IHotelClient, IDisposable
{
    private readonly RestClient client;

    /// <summary>
    /// Constructor.
    /// </summary>
    public HotelClient()
    {
        var option = new RestClientOptions("https://5f2be0b4ffc88500167b85a0.mockapi.io");

        client = new RestClient(option, configureSerialization: s => s.UseNewtonsoftJson());
    }

    /// <summary>
    /// Searches for hotels from Acme supplier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with a read-only collection of HotelAcmeDto.</returns>
    public async Task<RestResponse<IReadOnlyCollection<HotelAcmeDto>>> SearchHotelFromAcmeAsync()
    {
        var request = new RestRequest("suppliers/acme");

        var response = await client.ExecuteGetAsync<IReadOnlyCollection<HotelAcmeDto>>(request, CancellationToken.None);

        return response;
    }

    /// <summary>
    /// Searches for hotels from Patagonia supplier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with a read-only collection of HotelPatagoniaDto.</returns>
    public async Task<RestResponse<IReadOnlyCollection<HotelPatagoniaDto>>> SearchHotelFromPatagoniaAsync()
    {
        var request = new RestRequest("suppliers/patagonia");

        var response = await client.ExecuteGetAsync<IReadOnlyCollection<HotelPatagoniaDto>>(request, CancellationToken.None);

        return response;
    }

    /// <summary>
    /// Searches for hotels from PaperFlies supplier.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with a read-only collection of HotelPaperFliesDto.</returns>
    public async Task<RestResponse<IReadOnlyCollection<HotelPaperFliesDto>>> SearchHotelFromPaperFliesAsync()
    {
        var request = new RestRequest("suppliers/paperflies");

        var response = await client.ExecuteGetAsync<IReadOnlyCollection<HotelPaperFliesDto>>(request, CancellationToken.None);

        return response;
    }

    #region Dispose

    private bool disposed;

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the object and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">True if call from Dispose method, false if call from finalizer.</param>
    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                client.Dispose();
            }

            disposed = true;
        }
    }

    #endregion
}
