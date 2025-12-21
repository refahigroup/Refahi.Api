using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetReviews;

public class GetHottelReviewsByIdResponse
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("reviews")]
    public List<HotelReview> Reviews { get; set; } = new();
}

public class HotelReview
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("fullname")]
    public string Fullname { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("rate_overall")]
    public double Overall { get; set; }

    [JsonPropertyName("recommended")]
    public bool Recommended { get; set; }

    // بقیه فیلدها در صورت نیاز اضافه می‌شه
}
