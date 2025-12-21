using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Booking.ConfirmByCode;

public class BookingConfirmByIdCommand : CommandBase<string, BookingResponse>
{
    protected override string Endpoint => "/booking/{code}/confirm";

    public override async Task<BookingResponse> Execute(HttpClient client, string request)
    {
        string url = Endpoint.Replace("{code}", request);

        return await client.PostAsync<BookingResponse>(url, new { });
    }
}