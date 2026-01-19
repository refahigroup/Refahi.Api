using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Extensions;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCalendar;

public class GetAvailabilityCalendarByHotelIdCommand : CommandBase<GetAvailabilityCalendarByHotelIdRequest, GetAvailabilityCalendarByHotelIdResponse>
{
    protected override string Endpoint => "/availability/hotels/{id}/calendar";

    public override async Task<GetAvailabilityCalendarByHotelIdResponse> Execute(HttpClient client, GetAvailabilityCalendarByHotelIdRequest request)
    {
        string url = Endpoint.Replace("{id}", request.HotelId.ToString());
        url += $"?from={request.From.ToDateString()}&to={request.To.ToDateString()}";

        return await client.GetAsync<GetAvailabilityCalendarByHotelIdResponse>(url);
    }
}
