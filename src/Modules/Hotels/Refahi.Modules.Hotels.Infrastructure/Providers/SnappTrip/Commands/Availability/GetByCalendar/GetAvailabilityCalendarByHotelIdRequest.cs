using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCalendar;

public class GetAvailabilityCalendarByHotelIdRequest
{
    [JsonPropertyName("id")]
    public int HotelId { get; set; }

    [JsonPropertyName("from")]
    public DateOnly From { get; set; }

    [JsonPropertyName("to")]
    public DateOnly To { get; set; }
}