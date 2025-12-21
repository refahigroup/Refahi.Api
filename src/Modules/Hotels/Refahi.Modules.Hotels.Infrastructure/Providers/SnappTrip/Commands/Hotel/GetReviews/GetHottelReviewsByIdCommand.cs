using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetReviews;

public class GetHottelReviewsByIdCommand : CommandBase<IEnumerable<int>, GetHottelReviewsByIdResponse>
{
    protected override string Endpoint => "/hotels/reviews?id={id}";

    public override async Task<GetHottelReviewsByIdResponse> Execute(HttpClient client, IEnumerable<int> request)
    {
        string ids = string.Join(',', request.Select(x => x.ToString()));
        string url = Endpoint.Replace("{id}", ids);

        return await client.GetAsync<GetHottelReviewsByIdResponse>(url);
    }
}
