using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetAll;


public class CityInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title_fa")]
    public string TitleFa { get; set; } = string.Empty;

    [JsonPropertyName("title_en")]
    public string? TitleEn { get; set; }

    [JsonPropertyName("state")]
    public StateInfo? State { get; set; }
}

public class StateInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}