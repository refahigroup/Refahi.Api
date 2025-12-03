namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;

public class SnappTripAvailabilityResponse
{
    public IEnumerable<SnappTripHotelItem> Hotels { get; set; } = Enumerable.Empty<SnappTripHotelItem>();
}
