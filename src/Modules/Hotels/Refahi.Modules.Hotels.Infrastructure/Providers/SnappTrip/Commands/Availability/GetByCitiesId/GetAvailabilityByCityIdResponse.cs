namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCities;

using System.Text.Json.Serialization;
using System.Collections.Generic;

public class GetAvailabilityByCityIdResponse
{
    [JsonPropertyName("filter")]
    public AvailabilityByCitiesFilter? Filter { get; set; }

    [JsonPropertyName("items")]
    public List<AvailabilityByCitiesItem>? Items { get; set; }
}

public class AvailabilityByCitiesFilter
{
    [JsonPropertyName("min_price")]
    public int? MinPrice { get; set; }

    [JsonPropertyName("max_price")]
    public int? MaxPrice { get; set; }

    [JsonPropertyName("adults")]
    public int? Adults { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }

    [JsonPropertyName("available_rooms")]
    public int? AvailableRooms { get; set; }

    [JsonPropertyName("stars")]
    public List<int>? Stars { get; set; }

    [JsonPropertyName("accommodations")]
    public List<string>? Accommodations { get; set; }
}

public class AvailabilityByCitiesItem
{
    [JsonPropertyName("city_id")]
    public int CityId { get; set; }

    [JsonPropertyName("hotel")]
    public AvailabilityByCitiesHotel? Hotel { get; set; }

    [JsonPropertyName("room")]
    public AvailabilityByCitiesRoom? Room { get; set; }
}

public class AvailabilityByCitiesHotel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("accommodation_type")]
    public string? AccommodationType { get; set; }

    [JsonPropertyName("accommodation_title")]
    public string? AccommodationTitle { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("stars")]
    public int? Stars { get; set; }
}

public class AvailabilityByCitiesRoom
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("price_off")]
    public int? PriceOff { get; set; }

    [JsonPropertyName("discount_percent")]
    public int? DiscountPercent { get; set; }

    [JsonPropertyName("child_price")]
    public int? ChildPrice { get; set; }

    [JsonPropertyName("extra_bed_price")]
    public int? ExtraBedPrice { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }
}