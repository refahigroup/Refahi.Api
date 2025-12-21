using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;

namespace Refahi.Modules.Hotels.Application.Contract.Providers;

public interface IHotelProvider
{
    Task<IEnumerable<HotelSearchResultDto>> SearchHotelsAsync(SearchHotelsQuery query);

    Task<IEnumerable<HotelDetailsDto>> GetHotelDetailsAsync(GetHotelDetailsQuery query);

    Task<ProviderBookingCreateResultDto> CreateBookingAsync(BookingDraftDto dto);

    Task LockBookingAsync(string providerBookingCode);

    Task ConfirmBookingAsync(string providerBookingCode);

    Task<ProviderBookingStatusDto> GetBookingStatusAsync(string providerBookingCode);
}

