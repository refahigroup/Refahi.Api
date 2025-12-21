using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetDetails;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetFacilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetGaleries;


public class GetHotelGalleriesByIdResponse
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    [JsonPropertyName("gallery")]
    public List<Media> Gallery { get; set; } = new();
}
