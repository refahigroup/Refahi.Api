using MediatR;

namespace Refahi.Modules.Hotels.Application.Contract.Services.Statics.Cities;

public record GetCitiesRequest(string? CityName) : IRequest<IEnumerable<GetCitiesResponse>>;