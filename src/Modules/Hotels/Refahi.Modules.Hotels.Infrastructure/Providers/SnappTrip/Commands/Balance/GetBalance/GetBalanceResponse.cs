using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Balance.GetBalance;

public class GetBalanceResponse
{
    [JsonPropertyName("balance")]
    public long Balance { get; set; }
}