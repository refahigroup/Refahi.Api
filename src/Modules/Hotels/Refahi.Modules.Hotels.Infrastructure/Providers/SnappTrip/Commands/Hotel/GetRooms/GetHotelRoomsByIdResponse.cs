using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability;
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetRooms;

public class GetHotelRoomsByIdResponse
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("rooms")]
    public List<RoomDetail> Rooms { get; set; } = new();
}

public class RoomDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("accommodation_type")]
    public string Accommodation_type { get; set; }

    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("board_type")]
    public string BoardType { get; set; } = default!;

    [JsonPropertyName("adults")]
    public int Adults { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("facilities_tags")]
    public List<FacilityTag> FacilitiesTags { get; set; }

}


public class FacilityTag
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("facilities")]
    public List<Facility> Facilities { get; set; }

}