using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.GetByHotelId;

public class GetAvailabilityByHotelIdCommand: CommandBase<GetAvailabilityByHotelIdRequest, GetAvailabilityByHotelIdResponse>
{
    protected override string Endpoint => "/availability/hotels";

    public override async Task<GetAvailabilityByHotelIdResponse> Execute(HttpClient client, GetAvailabilityByHotelIdRequest request)
    {
        string url = Endpoint + $"?id={request.HotelId}&checkin={request.Checkin}&checkout={request.Checkout}";

        return await client.GetAsync<GetAvailabilityByHotelIdResponse>(url);
    }

}
