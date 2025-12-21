using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByRoomId;

public class GetAvailabilityByRoomIdResponse
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

    [JsonPropertyName("racks")]
    public List<RoomCalendarRack> Racks { get; set; } = new();
}

public class RoomCalendarRack
{
    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = string.Empty;
}