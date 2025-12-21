using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refahi.Modules.Hotels.Application.Contract.Providers;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCalendar;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCities;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.GetByCities;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Balance.GetBalance;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetAll;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetByCityId;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetDetails;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetFacilities;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetGaleries;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetReviews;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetRooms;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Config;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands;

public class Provider : IHotelProvider
{
    private readonly HttpClient _http;
    private readonly ILogger<Provider> _logger;
    private readonly SnappTripOptions _config;
    public Provider(HttpClient http, ILogger<Provider> logger, IOptions<SnappTripOptions> options)
    {
        _http = http;
        _logger = logger;
        _config = options.Value;

        _http.BaseAddress = new Uri(_config.BaseUrl);

        if (!_http.DefaultRequestHeaders.Contains("api-key"))
            _http.DefaultRequestHeaders.Add("api-key", _config.ApiKey);
    }


    public Task ConfirmBookingAsync(string providerBookingCode)
    {
        throw new NotImplementedException();
    }

    public Task<ProviderBookingCreateResultDto> CreateBookingAsync(BookingDraftDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ProviderBookingStatusDto> GetBookingStatusAsync(string providerBookingCode)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<HotelDetailsDto>> GetHotelDetailsAsync(GetHotelDetailsQuery query)
    {
        var cmd = new GetHotelDetailsByIdCommand();

        var response = await cmd.Execute(_http, new[] { (int)query.HotelId });

        return response.Select(x => new HotelDetailsDto
        {
            HotelId = x.Id,
            Name = x.Title,
            CityName = x.City?.Title ?? "",
            Description = x.Description ?? "",
            Address = x.Address ?? "",
            Stars = x.Stars ?? 0,
            Images = x.Gallery?.Select(g => g.Url) ?? Enumerable.Empty<string>(),
            Facilities = x.Facilities?.Select(f => f.Title) ?? Enumerable.Empty<string>(),
            //Rooms = response.Hotels[0].?.Select(r => new HotelRoomDto
            //{
            //    RoomId = r.Id,
            //    RoomName = r.Title ?? "",
            //    Description = r.Description,
            //    //MaxOccupancy = r.MaxOccupancy ?? 0,
            //    Facilities = r.Facilities?.Select(f => f.Title) ?? Enumerable.Empty<string>()
            //}) ?? Enumerable.Empty<HotelRoomDto>()
        });
    }

    public Task LockBookingAsync(string providerBookingCode)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<HotelSearchResultDto>> SearchHotelsAsync(SearchHotelsQuery query)
    {
        //var cmd = new GetAvailabilityByCityIdCommand();

        //var response = await cmd.Execute(_http, GetAvailabilityByCityIdRequest.Create(query));

        //return response.Items.Select(x => new HotelSearchResultDto
        //{
        //    HotelId = x.Hotel.Id,
        //    Name = x.Hotel.Title,
        //    CityId = x.CityId,
        //    Stars = x.Hotel.Stars ?? 0,
        //    MinPrice = (long)(x.Room?.PriceOff > 0 ? x.Room.PriceOff : x.Room?.Price ?? 0),
        //    //Currency = "IRR",
        //    //ThumbnailUrl = null // برای thumbnail بعداً می‌توانیم از galleries استفاده کنیم
        //});


        //////// City

        //var cmd = new GetAllCitiesCommand();
        //var response = await cmd.Execute(_http);

        //var cmd = new GetHotelsByCityIdCommand();
        //var response = await cmd.Execute(_http, 7454);


        //////// Hotel

        //var cmd = new GetHotelDetailsByIdCommand();
        //var response = await cmd.Execute(_http, new[] { 3245 });

        //var cmd = new GetHottelFacilitiesByIdCommand();
        //var response = await cmd.Execute(_http, new[] { 3245 });

        //var cmd = new GetHotelGalleriesByIdCommand();
        //var response = await cmd.Execute(_http, new[] { 3245 });

        // 403
        //var cmd = new GetHottelReviewsByIdCommand();
        //var response = await cmd.Execute(_http, new[] { 3245 });

        //var cmd = new GetHotelRoomsByIdCommand();
        //var response = await cmd.Execute(_http, new[] { 3245 });



        //////// Balance

        //var cmd = new GetBalanceCommand();
        //var response = await cmd.Execute(_http);


        //////// Availability

        var cmd = new GetAvailabilityCalendarByHotelIdCommand();
        var response = await cmd.Execute(_http, new GetAvailabilityCalendarByHotelIdRequest
        {
            HotelId = 3245,
            From = "2025-12-10",
            To = "2025-12-20"
        });


        return new List<HotelSearchResultDto>();
    }
}
