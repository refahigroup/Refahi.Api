using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Booking.LockByCode;

public class BookingLocoByIdCommand : CommandBase<string>
{
    protected override string Endpoint => "/booking/{code}/lock";

    public override async Task Execute(HttpClient client, string request)
    {
        string url = Endpoint.Replace("{code}", request);

        await client.PostNoContentAsync(url, new { });

    }
}