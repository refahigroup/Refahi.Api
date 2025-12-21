using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability;

public class HotelsAvailabilityResponse
{
    [JsonPropertyName("hotels")]
    public List<HotelAvailabilityItem> Hotels { get; set; } = new();
}

public class HotelAvailabilityItem
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("availability")]
    public List<HotelAvailabilityRoom> Availability { get; set; } = new();
}

public class HotelAvailabilityRoom
{
    [JsonPropertyName("room")]
    public AvailabilityRoomInfo Room { get; set; } = new();

    [JsonPropertyName("availability")]
    public int Availability { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    [JsonPropertyName("min_stay")]
    public int MinStay { get; set; }

    [JsonPropertyName("pricing")]
    public RoomPricing Pricing { get; set; } = new();

    [JsonPropertyName("racks")]
    public RoomRacks? Racks { get; set; }
}

public class AvailabilityRoomInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("accommodation_type")]
    public string? AccommodationType { get; set; }

    [JsonPropertyName("adults")]
    public int Adults { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }

    [JsonPropertyName("extra_bed")]
    public int? ExtraBed { get; set; }

    [JsonPropertyName("board_type")]
    public BoardType? BoardType { get; set; }

    [JsonPropertyName("facilities_tags")]
    public List<Facility>? FacilitiesTags { get; set; }
}

public class RoomPricing
{
    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("original_sell_price")]
    public int OriginalSellPrice { get; set; }

    [JsonPropertyName("discount_amount")]
    public int DiscountAmount { get; set; }

    [JsonPropertyName("extra_bed_price")]
    public int? ExtraBedPrice { get; set; }

    [JsonPropertyName("child_price")]
    public int? ChildPrice { get; set; }
}

public class RoomRacks
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("racks")]
    public List<RackPeriod>? Racks { get; set; }
}

public class RackPeriod
{
    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = string.Empty;
}

public enum BoardType
{
    [JsonPropertyName("room_only")]
    RoomOnly,

    [JsonPropertyName("bed_breakfast")]
    BedBreakfast,

    [JsonPropertyName("half_board")]
    HalfBoard,

    [JsonPropertyName("full_board")]
    FullBoard
}

public class Facility
{
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}