using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCalendar;

public class GetAvailabilityCalendarByHotelIdResponse
{
    [JsonPropertyName("id")]
    public int HotelId { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    [JsonPropertyName("racks")]
    public List<HotelCalendarRack> Racks { get; set; } = new();

    [JsonPropertyName("rooms")]
    public List<HotelCalendarRoom> Rooms { get; set; } = new();
}

public class HotelCalendarRack
{
    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = string.Empty;

    [JsonPropertyName("roomIDs")]
    public List<int> RoomIds { get; set; } = new();
}

public class HotelCalendarRoom
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("daily")]
    public Dictionary<string, DailyCalendarInfo> Daily { get; set; } = new();
}

public class DailyCalendarInfo
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("original_sell_price")]
    public int OriginalSellPrice { get; set; }

    [JsonPropertyName("discount_amount")]
    public int DiscountAmount { get; set; }

    [JsonPropertyName("availability")]
    public int Availability { get; set; }

    [JsonPropertyName("min_stay")]
    public int MinStay { get; set; }

    [JsonPropertyName("extra_bed_price")]
    public int? ExtraBedPrice { get; set; }

    [JsonPropertyName("child_price")]
    public int? ChildPrice { get; set; }
}