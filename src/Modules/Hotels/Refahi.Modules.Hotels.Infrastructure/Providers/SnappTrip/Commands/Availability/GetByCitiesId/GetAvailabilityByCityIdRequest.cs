using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs.Availability.AvailabilityByCity;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Extensions;
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCities;


public class GetAvailabilityByCityIdRequest
{
    [JsonPropertyName("city_id")]
    public int CityId { get; set; }

    [JsonPropertyName("checkin")]
    public string Checkin { get; set; }

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; }

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
    public int[]? Stars { get; set; }

    [JsonPropertyName("accommodations")]
    public string[]? Accommodations { get; set; }


    public static GetAvailabilityByCityIdRequest Create(GetAvailabilityByCityQuery dto)
    {
        return new GetAvailabilityByCityIdRequest
        {
            CityId = dto.CityId,
            Checkin = dto.CheckIn.ToDateString(),
            Checkout = dto.CheckOut.ToDateString(),
            Adults = dto.Adults,
            Children = dto.Children,
            AvailableRooms = dto.AvailableRooms,
            MinPrice = dto.MinPrice,
            MaxPrice = dto.MaxPrice,
            Stars = dto.Stars,
            Accommodations = dto.Accommodations
        };
    }
}