namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;

public class SnappTripBookingStatusResponse
{
    public string status { get; set; } = default!;
    public string? voucher_url { get; set; }
    public string? voucher_number { get; set; }
    public string? message { get; set; }
}
