using Microsoft.Extensions.Logging;
using Refahi.Modules.Hotels.Application.Contract.Providers;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contract;
using Refahi.Contract.Extensions;
using Refahi.Modules.Hotels.Application.Contract.Services.Statics.Cities;


namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip
{
    public class SnappTripProvider : IHotelProvider
    {
        private readonly SnappTripApiClient _apiClient;
        private readonly ILogger<SnappTripProvider> _logger;

        public SnappTripProvider(
            SnappTripApiClient apiClient,
            ILogger<SnappTripProvider> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        // ---------------------------------------------------------
        // SEARCH BY CITY  (در حال حاضر تست شده و کار می‌کند)
        // ---------------------------------------------------------
        public async Task<IEnumerable<HotelSearchResultDto>> SearchHotelsAsync(SearchHotelsQuery query)
        {
            var request = new SnappTripCityAvailabilityRequest
            {
                city_id = query.CityId,
                checkin = query.CheckIn.ToString("yyyy-MM-dd"),
                checkout = query.CheckOut.ToString("yyyy-MM-dd"),
                adults = query.Adults ?? 0,
                children = query.Children ?? 0,
                available_rooms = 1,
                min_price = 0,
                max_price = 0,
                stars = new List<int>(),
                accommodations = new List<string>()
            };

            var response = await _apiClient.SearchCityAvailabilityAsync(request);

            // Map از SnappTripCityAvailabilityResponse به HotelSearchResultDto
            return response.items.Select(x => new HotelSearchResultDto
            {
                HotelId = x.hotel.id,
                Name = x.hotel.title,
                CityId = x.city_id,
                Stars = x.hotel.stars,
                MinPrice = x.room.price_off > 0 ? x.room.price_off : x.room.price,
                //Currency = "IRR",
                //ThumbnailUrl = null // برای thumbnail بعداً می‌توانیم از galleries استفاده کنیم
            });
        }

        // ---------------------------------------------------------
        // FULL HOTEL DETAILS
        // ---------------------------------------------------------
        public async Task<IEnumerable<HotelDetailsDto>> GetHotelDetailsAsync(GetHotelDetailsQuery query)
        {
            var hotelId = query.HotelId;
            var checkIn = query.CheckIn.ToString("-");
            var checkOut = query.CheckOut.ToString("-");

            _logger.LogInformation("Fetching full hotel details for {HotelId} [{CheckIn} - {CheckOut}]",
                hotelId, checkIn, checkOut);

            // 1) Static + dynamic calls به صورت موازی
            var hotelTask = _apiClient.GetHotelDetailsAsync(hotelId);
            var roomsTask = _apiClient.GetHotelRoomsAsync(hotelId);
            var facilitiesTask = _apiClient.GetHotelFacilitiesAsync(hotelId);
            var galleriesTask = _apiClient.GetHotelGalleriesAsync(hotelId);
            var availabilityTask = _apiClient.GetHotelAvailabilityAsync(hotelId, checkIn, checkOut);

            await Task.WhenAll(hotelTask, roomsTask, facilitiesTask, galleriesTask, availabilityTask);

            var hotelRes = await hotelTask;
            var roomsRes = await roomsTask;
            var facilitiesRes = await facilitiesTask;
            var galleriesRes = await galleriesTask;
            var availabilityRes = await availabilityTask;

            var h = hotelRes.hotel;

            // Map availability روی roomId
            var availabilityByRoomId = availabilityRes.availability
                .GroupBy(a => a.room.id)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList()
                );

            // Facilities: flatten
            var facilityTitles = facilitiesRes.facilities
                .Select(f => f.title)
                .Distinct()
                .ToList();

            // تصاویر
            var galleryUrls = new List<string>();

            if (h.cover != null && !string.IsNullOrWhiteSpace(h.cover.url))
                galleryUrls.Add(h.cover.url);

            if (galleriesRes.First().gallery is { Count: > 0 })
                galleryUrls.AddRange(galleriesRes.First().gallery.Select(x => x.url));

            // Rooms
            var roomDtos = new List<HotelRoomDto>();

            foreach (var room in roomsRes.rooms)
            {
                availabilityByRoomId.TryGetValue(room.id, out var roomAvailabilityList);

                // یک رکورد representative برای قیمت انتخاب می‌کنیم (مثلاً کمترین price)
                var pricing = roomAvailabilityList?
                    .Select(a => a.pricing)
                    .OrderBy(p => p.price)
                    .FirstOrDefault();

                var price = pricing?.price ?? 0;
                var originalPrice = pricing?.original_sell_price ?? price;
                var discountAmount = pricing?.discount_amount ?? 0;
                var childPrice = pricing?.child_price ?? 0;
                var extraBedPrice = pricing?.extra_bed_price ?? 0;

                var roomFacilities = room.facilities_tags?
                    .SelectMany(t => t.facilities)
                    .Select(f => f.title)
                    .Distinct()
                    .ToList() ?? new List<string>();

                var dto = new HotelRoomDto
                {
                    RoomId = room.id,
                    RoomName = room.title,
                    Description = room.description,
                    Adults = room.adults,
                    Children = room.children,
                    BoardType = room.board_type,
                    //AccommodationType = availabilityByRoomId.ContainsKey(room.id)
                    //    ? availabilityByRoomId[room.id].First().room.accommodation_type
                    //    : h.accommodation_type,
                    Facilities = roomFacilities,
                    //BasePrice = price,
                    //OriginalPrice = originalPrice,
                    //DiscountAmount = discountAmount,
                    //ChildPrice = childPrice,
                    //ExtraBedPrice = extraBedPrice,
                    //Currency = "IRR"
                };

                roomDtos.Add(dto);
            }

            var result = new HotelDetailsDto
            {
                HotelId = h.id,
                Name = h.title,
                //CityId = h.city.id,
                CityName = h.city.title,
                Stars = h.stars,
                Address = h.address,
                Description = h.description,
                //AccommodationTitle = h.accommodation_title,
                //AccommodationType = h.accommodation_type,
                Facilities = facilityTitles,
                Images = galleryUrls.Distinct().ToList(),
                Rooms = roomDtos
            };

            return new[] { result };
        }

        // ---------------------------------------------------------
        // BOOKING LIFECYCLE – این‌ها را بعد از نهایی شدن DTOهای Application تکمیل می‌کنیم
        // ---------------------------------------------------------

        public Task<ProviderBookingCreateResultDto> CreateBookingAsync(BookingDraftDto request)
        {
            // TODO: map BookingDraftDto به SnappTripCreateBookingRequest و call _apiClient.CreateBookingAsync
            throw new NotImplementedException();
        }

        public Task LockBookingAsync(string providerBookingCode)
        {
            // TODO: call _apiClient.LockBookingAsync(providerBookingCode)
            throw new NotImplementedException();
        }

        public Task ConfirmBookingAsync(string providerBookingCode)
        {
            // TODO: call _apiClient.ConfirmBookingAsync(providerBookingCode)
            throw new NotImplementedException();
        }

        public Task<ProviderBookingStatusDto> GetBookingStatusAsync(string providerBookingCode)
        {
            // TODO: call _apiClient.GetBookingStatusAsync(providerBookingCode)
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetCitiesResponse>> GetAllCities(string? name)
        {
            throw new NotImplementedException();
        }
    }
}
