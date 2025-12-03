using MediatR;
using Refahi.Modules.Hotels.Application.Contract.Providers;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;

namespace Refahi.Modules.Hotels.Application.Hotels.Search
{
    public sealed class SearchHotelsQueryHandler: IRequestHandler<SearchHotelsQuery, IEnumerable<HotelSearchResultDto>>
    {
        private readonly IHotelProvider _provider;

        public SearchHotelsQueryHandler(IHotelProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<HotelSearchResultDto>> Handle(
            SearchHotelsQuery request,
            CancellationToken cancellationToken)
        {
            return await _provider.SearchHotelsAsync(request);
        }
    }
}
