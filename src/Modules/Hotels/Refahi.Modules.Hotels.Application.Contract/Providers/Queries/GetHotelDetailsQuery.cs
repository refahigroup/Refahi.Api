using MediatR;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

namespace Refahi.Modules.Hotels.Application.Contract.Providers.Queries;

public sealed record GetHotelDetailsQuery(
    long HotelId,
    DateOnly? CheckIn,
    DateOnly? CheckOut
) : IRequest<IEnumerable<HotelDetailsDto>>;