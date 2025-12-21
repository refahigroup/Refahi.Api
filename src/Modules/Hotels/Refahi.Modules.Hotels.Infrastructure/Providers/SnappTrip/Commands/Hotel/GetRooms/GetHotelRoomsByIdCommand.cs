using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetRooms;

public class GetHotelRoomsByIdCommand : CommandBase<IEnumerable<int>, IEnumerable<GetHotelRoomsByIdResponse>>
{
    protected override string Endpoint => "/hotels/rooms?id={id}";

    public override async Task<IEnumerable<GetHotelRoomsByIdResponse>> Execute(HttpClient client, IEnumerable<int> request)
    {
        string ids = string.Join(',', request.Select(x => x.ToString()));
        string url = Endpoint.Replace("{id}", ids);

        return await client.GetAsync<IEnumerable<GetHotelRoomsByIdResponse>>(url);
    }
}
