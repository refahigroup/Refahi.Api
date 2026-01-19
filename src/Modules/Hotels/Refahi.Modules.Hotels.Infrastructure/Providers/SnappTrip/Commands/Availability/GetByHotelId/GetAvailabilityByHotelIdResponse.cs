using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contract;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.GetByHotelId;

public sealed class GetAvailabilityByHotelIdResponse
{
    public int hotel_id { get; set; }
    public List<GetAvailabilityByHotelIdRoomAvailability> availability { get; set; } = new();
}

public sealed class GetAvailabilityByHotelIdRoomAvailability
{
    public int availability { get; set; }
    public string from { get; set; } = default!;
    public string to { get; set; } = default!;
    public int min_stay { get; set; }

    public RoomPricing pricing { get; set; } = new();
    public RoomRacks racks { get; set; } = new();

    public GetAvailabilityByHotelIdRoom room { get; set; } = new();
}

public sealed class GetAvailabilityByHotelIdRoomPricing
{
    public int discount_amount { get; set; }
    public int child_price { get; set; }
    public int extra_bed_price { get; set; }
    public int original_sell_price { get; set; }
    public int price { get; set; }
}

public sealed class GetAvailabilityByHotelIdRoomRacks
{
    public string title { get; set; } = default!;
    public List<SnappTripRack> racks { get; set; } = new();
}

public sealed class GetAvailabilityByHotelIdRack
{
    public string checkin { get; set; } = default!;
    public string checkout { get; set; } = default!;
}

public sealed class GetAvailabilityByHotelIdRoom
{
    public int id { get; set; }
    public string title { get; set; } = default!;
    public int adults { get; set; }
    public int children { get; set; }
    public string description { get; set; } = default!;
    public string accommodation_type { get; set; } = default!;

    public int extra_bed { get; set; }

    public List<GetAvailabilityByHotelIdRoomFacilityTag> facilities_tags { get; set; } = new();
}

public sealed class GetAvailabilityByHotelIdRoomFacilityTag
{
    public int hotel_id { get; set; }
    public List<SnappTripFacility> facilities { get; set; } = new();
}
