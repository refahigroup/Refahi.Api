using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetGaleries;

public class GetHotelGalleriesByIdCommand : CommandBase<IEnumerable<int>, IEnumerable<GetHotelGalleriesByIdResponse>>
{
    protected override string Endpoint => "/hotels/galleries?id={id}";

    public override async Task<IEnumerable<GetHotelGalleriesByIdResponse>> Execute(HttpClient client, IEnumerable<int> request)
    {
        string ids = string.Join(',', request.Select(x => x.ToString()));
        string url = Endpoint.Replace("{id}", ids);

        return await client.GetAsync<IEnumerable<GetHotelGalleriesByIdResponse>>(url);
    }
}
