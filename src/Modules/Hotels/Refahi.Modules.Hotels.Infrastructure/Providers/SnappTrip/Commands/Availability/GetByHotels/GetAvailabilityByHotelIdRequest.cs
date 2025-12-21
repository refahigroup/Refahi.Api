using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByHotels;

public class GetAvailabilityByHotelIdRequest
{
    [JsonPropertyName("id")]
    public IEnumerable<int> Ids { get; set; }

    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = default!;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = default!;
}
