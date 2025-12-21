using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCities;


public class GetAvailabilityByCityIdRequest
{
    [JsonPropertyName("city_id")]
    public int CityId { get; set; }

    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = string.Empty;

    [JsonPropertyName("adults")]
    public int? Adults { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }

    [JsonPropertyName("available_rooms")]
    public int? AvailableRooms { get; set; }

    [JsonPropertyName("min_price")]
    public int? MinPrice { get; set; }

    [JsonPropertyName("max_price")]
    public int? MaxPrice { get; set; }

    [JsonPropertyName("stars")]
    public List<int>? Stars { get; set; }

    [JsonPropertyName("accommodations")]
    public List<string>? Accommodations { get; set; }


    public static GetAvailabilityByCityIdRequest Create(SearchHotelsQuery dto)
    {
        return new GetAvailabilityByCityIdRequest
        {
            CityId = dto.CityId,
            Checkin = dto.CheckIn.ToString("yyyy-MM-dd"),
            Checkout = dto.CheckOut.ToString("yyyy-MM-dd"),
            Adults = dto.Adults,
            Children = dto.Children,
            AvailableRooms = 1,
            MinPrice = 0,
            MaxPrice = 0,
            Stars = new List<int>(),
            Accommodations = new List<string>()
        };
    }
}