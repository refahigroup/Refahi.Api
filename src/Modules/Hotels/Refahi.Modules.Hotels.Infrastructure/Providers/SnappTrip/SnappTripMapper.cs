using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip;

public static class SnappTripMapper
{
    public static IEnumerable<HotelSearchResultDto> MapSearchResults(SnappTripAvailabilityResponse dto)
    {
        return dto.Hotels.Select(h => new HotelSearchResultDto
        {
            HotelId = h.hotel_id,
            Name = h.name,
            CityName = h.city_name,
            Stars = h.stars,
            AccommodationType = h.accommodation_type,
            MinCustomerPrice = h.min_price,
            ThumbnailUrl = h.thumbnail
        });
    }

    public static HotelDetailsDto MapHotelDetails(SnappTripHotelDetailsResponse h)
    {
        return new HotelDetailsDto
        {
            HotelId = h.hotel_id,
            Name = h.name,
            Description = h.description,
            Address = h.address,
            Images = h.images,
            Facilities = h.facilities,
            Rooms = h.rooms.Select(r => new HotelRoomDto
            {
                RoomId = r.room_id,
                RoomName = r.room_name,
                Capacity = r.capacity,
                CustomerPrice = r.price,
                BoardType = r.board_type
            })
        };
    }

    public static ProviderBookingCreateResultDto MapCreateBooking(SnappTripBookingCreateResponse r)
    {
        return new ProviderBookingCreateResultDto
        {
            ProviderBookingCode = r.code,
            ProviderPrice = r.price,
            Currency = "IRT",
            LockedUntil = r.lock_seconds.HasValue
                ? DateTime.UtcNow.AddSeconds(r.lock_seconds.Value)
                : null
        };
    }

    public static ProviderBookingStatusDto MapStatus(SnappTripBookingStatusResponse r)
    {
        return new ProviderBookingStatusDto
        {
            Status = r.status,
            VoucherUrl = r.voucher_url,
            VoucherNumber = r.voucher_number,
            ProviderMessage = r.message
        };
    }
}
