using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetAll;

public class GetAllCitiesCommand : CommandBase<int, IEnumerable<CityInfo>>
{
    protected override string Endpoint => "/cities";

    public override async Task<IEnumerable<CityInfo>> Execute(HttpClient client, int request = 0)
    {
        return await client.GetAsync<IEnumerable<CityInfo>>(Endpoint);
    }
}
