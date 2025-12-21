
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Booking.GetByCode;


public class GetBookingByCodeCommand : CommandBase<string, BookingResponse>
{
    protected override string Endpoint => "/booking/{code}";

    public override async Task<BookingResponse> Execute(HttpClient client, string request)
    {
        string url = Endpoint.Replace("{code}", request);

        return await client.GetAsync<BookingResponse>(url);
    }
}