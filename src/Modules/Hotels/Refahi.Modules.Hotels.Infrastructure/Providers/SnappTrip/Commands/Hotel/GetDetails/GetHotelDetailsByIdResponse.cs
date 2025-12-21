using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetAll;
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetDetails;

public class GetHotelDetailsByIdResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("title_en")]
    public string? TitleEn { get; set; }

    [JsonPropertyName("accommodation_type")]
    public string? AccommodationType { get; set; }

    [JsonPropertyName("accommodation_title")]
    public string? AccommodationTitle { get; set; }

    [JsonPropertyName("stars")]
    public int? Stars { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    [JsonPropertyName("is_marketplace")]
    public bool IsMarketplace { get; set; }

    [JsonPropertyName("city")]
    public HotelDetailsCityInfo? City { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("cover")]
    public Media? Cover { get; set; }

    [JsonPropertyName("gallery")]
    public List<Media> Gallery { get; set; } = new();

    [JsonPropertyName("facilities")]
    public List<Facility> Facilities { get; set; } = new();

    [JsonPropertyName("policies")]
    public HotelPolicies? Policies { get; set; }

    [JsonPropertyName("reviews")]
    public HotelRatings? Reviews { get; set; }
}

public class Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}

public class Media
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class HotelPolicies
{
    [JsonPropertyName("check_in_time")]
    public string? CheckInTime { get; set; }

    [JsonPropertyName("check_out_time")]
    public string? CheckOutTime { get; set; }

    [JsonPropertyName("cancellation")]
    public string? Cancellation { get; set; }

    [JsonPropertyName("child_age")]
    public int? ChildAge { get; set; }

    [JsonPropertyName("infant_age")]
    public int? InfantAge { get; set; }

    [JsonPropertyName("foreigners_fee")]
    public bool? ForeignersFee { get; set; }

    [JsonPropertyName("free_transfer_policy")]
    public string? FreeTransferPolicy { get; set; }

    [JsonPropertyName("free_transfers")]
    public List<string>? FreeTransfers { get; set; }
}

public class HotelRatings
{
    [JsonPropertyName("ratings")]
    public string? Ratings { get; set; }

    [JsonPropertyName("reviews")]
    public string? Reviews { get; set; }
}

public class HotelDetailsCityInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}
