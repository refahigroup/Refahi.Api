using Microsoft.Extensions.Options;
using Refahi.Modules.Hotels.Application.Contract.Providers;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Config;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip
{
    public class SnappTripProvider : IHotelProvider
    {
        private readonly SnappTripApiClient _api;
        private readonly SnappTripOptions _options;

        public SnappTripProvider(
            SnappTripApiClient api,
            IOptions<SnappTripOptions> options)
        {
            _api = api;
            _options = options.Value;
        }

        public async Task<IEnumerable<HotelSearchResultDto>> SearchHotelsAsync(SearchHotelsQuery query)
        {
            var dto = await _api.SearchHotelsAsync(query.CityId, query.CheckIn, query.CheckOut);
            return SnappTripMapper.MapSearchResults(dto);
        }

        public async Task<HotelDetailsDto> GetHotelDetailsAsync(GetHotelDetailsQuery query)
        {
            var dto = await _api.GetHotelDetailsAsync(query.HotelId);
            return SnappTripMapper.MapHotelDetails(dto);
        }

        public async Task<ProviderBookingCreateResultDto> CreateBookingAsync(BookingDraftDto request)
        {
            var body = new
            {
                hotel_id = request.HotelId,
                room_id = request.RoomId,
                checkin = request.CheckIn.ToString("yyyy-MM-dd"),
                checkout = request.CheckOut.ToString("yyyy-MM-dd"),
                rooms = request.RoomsCount,
                guests = request.Guests.Select(g => new
                {
                    name = g.FullName,
                    age = g.Age,
                    type = g.Type
                }),
                board_type = request.BoardType
            };

            var result = await _api.CreateBookingAsync(body);
            return SnappTripMapper.MapCreateBooking(result);
        }

        public Task LockBookingAsync(string providerBookingCode)
            => _api.LockBookingAsync(providerBookingCode);

        public Task ConfirmBookingAsync(string providerBookingCode)
            => _api.ConfirmBookingAsync(providerBookingCode);

        public async Task<ProviderBookingStatusDto> GetBookingStatusAsync(string providerBookingCode)
        {
            var status = await _api.GetBookingStatusAsync(providerBookingCode);
            return SnappTripMapper.MapStatus(status);
        }
    }
}
