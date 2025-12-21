using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByRoomId;

public class GetAvailabilityByRoomIdCommand : CommandBase<GetAvailabilityByRoomIdRequest, IEnumerable<GetAvailabilityByRoomIdResponse>>
{
    protected override string Endpoint => "/availability/hotels/{id}/room/{room_id}/calendar";

    public override async Task<IEnumerable<GetAvailabilityByRoomIdResponse>> Execute(HttpClient client, GetAvailabilityByRoomIdRequest request)
    {
        string url = Endpoint.Replace("{id}", request.HotelId.ToString())
                             .Replace("{room_id}", request.RoomId.ToString());
            
        url += $"?from={request.From}&to={request.To}";

        return await client.GetAsync<List<GetAvailabilityByRoomIdResponse>>(url);
    }
}

