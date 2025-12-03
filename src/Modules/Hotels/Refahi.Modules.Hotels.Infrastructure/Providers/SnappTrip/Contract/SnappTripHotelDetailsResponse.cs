namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;

public class SnappTripHotelDetailsResponse
{
    public long hotel_id { get; set; }
    public string name { get; set; } = default!;
    public string description { get; set; } = default!;
    public string address { get; set; } = default!;
    public IEnumerable<string> images { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<string> facilities { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<SnappTripRoomItem> rooms { get; set; } = Enumerable.Empty<SnappTripRoomItem>();
}
