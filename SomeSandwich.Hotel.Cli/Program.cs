using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SomeSandwich.Hotel.Cli.Clients;
using SomeSandwich.Hotel.Cli.Services;

namespace SomeSandwich.Hotel.Cli;

internal sealed class Program
{
    private static readonly IHotelMergerService HotelMergerService = new HotelMergerService(new HotelClient());

    /// <summary>
    /// Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static async Task<int> Main(string[] args)
    {
        if (args.Length is < 2 or > 2)
        {
            Console.WriteLine(
            """
            The input requires two lists of values separated by a comma: hotel_ids and destination_ids.
            Example: SomeSandwich.Hotel.Cli iJhz,SjyX,f8c9 5432,5432,1122
            """);
            return 1;
        }

        // Get the hotel ids and destination ids from the input arguments.
        var hotelIds = new List<string>();
        if (args[0] != "none")
        {
            hotelIds = args[0]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToList();
        }

        var destinationIds = new List<int>();
        if (args[1] != "none")
        {
            try
            {
                destinationIds
                    .AddRange(args[1].Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse));
                destinationIds = destinationIds
                    .Where(v => v != 0)
                    .Distinct()
                    .ToList();
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    """
                    The format of destination_ids is not correct, it must be a list of numbers separated by commas.
                    Example: SomeSandwich.Hotel.Cli iJhz,SjyX,f8c9 5432,5432,1122
                    """);
                return 1;
            }
            catch (Exception)
            {
                Console.WriteLine(
                    """
                    There are some error. Please running application with correct format.
                    Example: SomeSandwich.Hotel.Cli iJhz,SjyX,f8c9 5432,5432,1122
                    """);
                return 1;
            }
        }

        var hotels = await HotelMergerService.SearchMergeHotelAsync(hotelIds, destinationIds);

        // Print the hotels json string.
        var hotelsStr = JsonConvert.SerializeObject(hotels, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
            Formatting = Formatting.Indented
        });
        Console.WriteLine(hotelsStr);

        return 0;
    }
}
