using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs.Availability.AvailabilityByCity;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using Refahi.Modules.Hotels.Application.Contract.Services.Statics.Cities;

namespace Refahi.Modules.Hotels.Application.Contract.Providers;

public interface IHotelProvider
{

    // Availability
    Task<GetAvailabilityByCityDto> GetAvailabilityByCity(GetAvailabilityByCityQuery query);








    Task<IEnumerable<HotelDetailsDto>> GetHotelDetailsAsync(GetHotelDetailsQuery query);

    Task<ProviderBookingCreateResultDto> CreateBookingAsync(BookingDraftDto dto);

    Task LockBookingAsync(string providerBookingCode);

    Task ConfirmBookingAsync(string providerBookingCode);

    Task<ProviderBookingStatusDto> GetBookingStatusAsync(string providerBookingCode);



    Task<IEnumerable<GetCitiesResponse>> GetAllCities(string? name);
}

