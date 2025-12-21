using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetDetails;

public class GetHotelDetailsByIdCommand : CommandBase<IEnumerable<int>, IEnumerable<GetHotelDetailsByIdResponse>>
{
    protected override string Endpoint => "/hotels?id={id}";

    public override async Task<IEnumerable<GetHotelDetailsByIdResponse>> Execute(HttpClient client, IEnumerable<int> request)
    {
        string ids = string.Join(',', request.Select(x => x.ToString()));
        string url = Endpoint.Replace("{id}", ids);

        return await client.GetAsync<IEnumerable<GetHotelDetailsByIdResponse>>(url);
    }
}