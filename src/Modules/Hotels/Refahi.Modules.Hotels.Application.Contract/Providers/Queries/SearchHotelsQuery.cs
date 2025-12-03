using MediatR;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

namespace Refahi.Modules.Hotels.Application.Contract.Providers.Queries;

public sealed record SearchHotelsQuery(
        int CityId,
        DateOnly CheckIn,
        DateOnly CheckOut,
        int Adults,
        int Children
    ) : IRequest<IEnumerable<HotelSearchResultDto>>;

