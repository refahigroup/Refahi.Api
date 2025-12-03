namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;

public class SnappTripRoomItem
{
    public long room_id { get; set; }
    public string room_name { get; set; } = default!;
    public int capacity { get; set; }
    public long price { get; set; }
    public string board_type { get; set; } = default!;
}
