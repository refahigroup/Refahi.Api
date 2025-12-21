using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByHotels;

public class GetAvailabilityByHotelIdCommand : CommandBase<GetAvailabilityByHotelIdRequest, HotelsAvailabilityResponse>
{
    protected override string Endpoint => "/availability/cities";

    public override async Task<HotelsAvailabilityResponse> Execute(HttpClient client, GetAvailabilityByHotelIdRequest request)
    {
        string ids = string.Join(',', request.Ids.Select(x => x.ToString()));

        string url = Endpoint + $"?id={ids}&checkin={request.Checkin}&checkout={request.Checkout}";

        return await client.GetAsync<HotelsAvailabilityResponse>(url);
    }
}
