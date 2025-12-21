using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetByCityId;

public class GetHotelsByCityIdResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("city_id")]
    public int CityId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("fa_url")]
    public string? FaUrl { get; set; }
}