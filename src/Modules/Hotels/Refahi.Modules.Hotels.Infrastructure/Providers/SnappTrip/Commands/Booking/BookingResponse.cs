using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Booking.Create;
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Booking;

public class BookingResponse
{
    [JsonPropertyName("reservation_code")]
    public string ReservationCode { get; set; } = string.Empty;

    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public long Price { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("rooms")]
    public List<BookingRoomResponse> Rooms { get; set; } = new();
}

public class BookingRoomResponse
{
    [JsonPropertyName("room_id")]
    public int RoomId { get; set; }

    [JsonPropertyName("guests")]
    public List<BookingGuest> Guests { get; set; } = new();

    [JsonPropertyName("extra_beds")]
    public int? ExtraBeds { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }

    [JsonPropertyName("infants")]
    public int? Infants { get; set; }
}

public class BookingGuest
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("foreigner")]
    public bool Foreigner { get; set; }
}