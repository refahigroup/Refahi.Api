
namespace Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

public sealed class HotelSearchResultDto
{
    public long HotelId { get; set; }
    public string Name { get; set; } = default!;
    public string CityName { get; set; } = default!;
    public string AccommodationType { get; set; } = default!;
    public int Stars { get; set; }
    public long MinCustomerPrice { get; set; }
    public string ThumbnailUrl { get; set; } = default!;
}


