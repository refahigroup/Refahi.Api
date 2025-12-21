using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCities;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.GetByCities;


public class GetAvailabilityByCityIdCommand : CommandBase<GetAvailabilityByCityIdRequest, GetAvailabilityByCityIdResponse>
{
    protected override string Endpoint => "/availability/cities";

    public override async Task<GetAvailabilityByCityIdResponse> Execute(HttpClient client, GetAvailabilityByCityIdRequest request)
    {
        return await client.PostAsync<GetAvailabilityByCityIdResponse>(Endpoint, request);
    }
}

