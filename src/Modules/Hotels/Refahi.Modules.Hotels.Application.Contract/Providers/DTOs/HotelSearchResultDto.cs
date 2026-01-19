
using System.Text.Json.Serialization;

namespace Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

public sealed record HotelSearchResultDto
(
    int HotelId,
    string Name,
    int CityId,
    int? Stars,
    decimal MinPrice
);