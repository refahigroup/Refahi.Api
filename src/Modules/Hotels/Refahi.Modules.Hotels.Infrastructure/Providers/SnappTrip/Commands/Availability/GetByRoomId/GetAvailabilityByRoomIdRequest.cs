using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByRoomId;

public class GetAvailabilityByRoomIdRequest
{
    [JsonPropertyName("id")]
    public int HotelId { get; set; }

    [JsonPropertyName("room_id")]
    public int RoomId { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;
}
