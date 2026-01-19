using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refahi.Modules.Hotels.Application.Contract.Providers;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;
using Refahi.Modules.Hotels.Application.Contract.Providers.DTOs.Availability.AvailabilityByCity;
using Refahi.Modules.Hotels.Application.Contract.Providers.Queries;
using Refahi.Modules.Hotels.Application.Contract.Services.Statics.Cities;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.ByCities;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.GetByCities;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Cities.GetAll;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Hotel.GetDetails;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Config;
using AvailabilityByCity = Refahi.Modules.Hotels.Application.Contract.Providers.DTOs.Availability.AvailabilityByCity;

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


    // Availability
    public async Task<GetAvailabilityByCityDto> GetAvailabilityByCity(GetAvailabilityByCityQuery query)
    {
        var cmd = new GetAvailabilityByCityIdCommand();

        var response = await cmd.Execute(_http, GetAvailabilityByCityIdRequest.Create(query));

        var result = GetAvailabilityByCityDto.Create(query);

        if(response.Items != null)
        {
            result.Items = response.Items.Select(x => new AvailabilityByCity.AvailabilityByCitiesItem
            (
                x.CityId,
                (x.Hotel == null)
                    ? null
                    : new AvailabilityByCity.AvailabilityByCitiesHotel
                    (
                        x.Hotel.Id,
                        x.Hotel.Title,
                        x.Hotel.AccommodationType,
                        x.Hotel.AccommodationTitle,
                        x.Hotel.Address,
                        x.Hotel.Stars
                    ),
                (x.Room == null)
                    ? null
                    : new AvailabilityByCity.AvailabilityByCitiesRoom
                    (
                        x.Room.Id,
                        x.Room.Title,
                        x.Room.Price,
                        x.Room.PriceOff,
                        x.Room.DiscountPercent,
                        x.Room.ChildPrice,
                        x.Room.ExtraBedPrice,
                        x.Room.Children
                    )
            )).ToList();
        }

        return result;

    }
    public async Task<>



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

    public async Task<IEnumerable<GetCitiesResponse>> GetAllCities(string? name)
    {
        var cmd = new GetAllCitiesCommand();
        var response = await cmd.Execute(_http);

        var result = !string.IsNullOrEmpty(name)
            ? response.Where(x => x.TitleFa.StartsWith(name) || x.TitleEn.StartsWith(name))
            : response;

        return result.Select(x => new GetCitiesResponse(
            x.Id,
            x.TitleFa,
            x.TitleEn ?? "",
            x.State.Id,
            x.State.Title
        ));

    }

}
