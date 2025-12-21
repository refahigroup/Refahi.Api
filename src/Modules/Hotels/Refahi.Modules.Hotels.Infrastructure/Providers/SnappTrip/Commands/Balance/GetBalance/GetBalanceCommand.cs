using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Balance.GetBalance;


public class GetBalanceCommand : CommandBase<int, GetBalanceResponse>
{
    protected override string Endpoint => "/balance";

    public override async Task<GetBalanceResponse> Execute(HttpClient client, int request = 0)
    {
        return await client.GetAsync<GetBalanceResponse>(Endpoint);
    }
}
