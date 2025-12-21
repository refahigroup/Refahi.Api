using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetFacilities;

public class GetHottelFacilitiesByIdCommand : CommandBase<IEnumerable<int>, IEnumerable<GetHottelFacilitiesByIdResponse>>
{
    protected override string Endpoint => "/hotels/facilities?id={id}";

    public override async Task<IEnumerable<GetHottelFacilitiesByIdResponse>> Execute(HttpClient client, IEnumerable<int> request)
    {
        string ids = string.Join(',', request.Select(x => x.ToString()));
        string url = Endpoint.Replace("{id}", ids);

        return await client.GetAsync<IEnumerable<GetHottelFacilitiesByIdResponse>>(url);
    }
}
