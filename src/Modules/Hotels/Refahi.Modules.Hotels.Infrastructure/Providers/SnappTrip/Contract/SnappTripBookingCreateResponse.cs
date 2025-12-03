namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;

public class SnappTripBookingCreateResponse
{
    public string code { get; set; } = default!;
    public long price { get; set; }
    public long? lock_seconds { get; set; }
}
