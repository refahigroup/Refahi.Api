using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Booking.Create;

public class CreateBookingCommand : CommandBase<int, BookingResponse>
{
    protected override string Endpoint => "/booking/create";

    public override async Task<BookingResponse> Execute(HttpClient client, int request = 0)
    {
        return await client.PostAsync<BookingResponse>(Endpoint, new { });
    }
}