using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetByCityId;

public class GetHotelsByCityIdCommand: CommandBase<int, IEnumerable<GetHotelsByCityIdResponse>>
{
    protected override string Endpoint => "/cities/{id}/hotels";

    public override async Task<IEnumerable<GetHotelsByCityIdResponse>> Execute(HttpClient client, int request)
    {
        string url = Endpoint.Replace("{id}", request.ToString());

        return await client.GetAsync<IEnumerable<GetHotelsByCityIdResponse>>(url);
    }
}
