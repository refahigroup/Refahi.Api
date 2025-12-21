using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability;
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetFacilities;

public class GetHottelFacilitiesByIdResponse
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("facilities")]
    public List<Facility> Facilities { get; set; } = new();
}
