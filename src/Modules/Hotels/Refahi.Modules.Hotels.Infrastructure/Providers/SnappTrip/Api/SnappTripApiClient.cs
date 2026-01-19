using Microsoft.Extensions.Logging;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contract;
using System.Net.Http.Json;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

/*
    ** Availability
        1) /availability/cities [OK]
        2) /availability/hotels [OK]
        3) /availability/hotels/{id}
        4) /availability/hotels/{id}/calendar
        5) /availability/hotels/{id}/room/calendar

    ** Balance
        6) /balance
    
    ** Booking
        7) /booking/create [OK]
        8) /booking/{code} [OK]
        9) /booking/{code}/confirm [OK]
        10) /booking/{code}/lock [OK]

    ** Cities
        11) /cities
        12) /cities/{id}/hotels

    ** Health
        13) /health

    ** Hotels
        14) /hotels [OK]
        15) /hotels/facilities [OK]
        16) /hotels/galleries [OK]
        17) /hotels/rooms [OK]

*/

public class SnappTripApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<SnappTripApiClient> _logger;

    public SnappTripApiClient(HttpClient http, ILogger<SnappTripApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }


    #region INTERNAL HELPERS

    private async Task<T> GetAsync<T>(string url)
    {
        _logger.LogInformation("SnappTrip GET {Url}", url);

        var response = await _http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            await ThrowApiError(response, url);

        string json = await response.Content.ReadAsStringAsync();

        _logger.LogInformation(json);

        var result = await response.Content.ReadFromJsonAsync<T>();
        if (result == null)
            throw new Exception($"SnappTrip GET {url} returned NULL");

        return result;
    }

    private async Task<T> PostAsync<T>(string url, object payload)
    {
        _logger.LogInformation("SnappTrip POST {Url}", url);

        var response = await _http.PostAsJsonAsync(url, payload);

        if (!response.IsSuccessStatusCode)
            await ThrowApiError(response, url);

        string json = await response.Content.ReadAsStringAsync();

        var result = await response.Content.ReadFromJsonAsync<T>();
        if (result == null)
            throw new Exception($"SnappTrip POST {url} returned NULL");

        return result;
    }

    private async Task ThrowApiError(HttpResponseMessage response, string url)
    {
        var err = await response.Content.ReadFromJsonAsync<SnappTripApiError>();
        var message = $"SnappTrip Error calling {url}. " +
                      $"Status={(int)response.StatusCode}, " +
                      $"Code={err?.code}, " +
                      $"Message={err?.message}, Trace={err?.trace_id}";

        _logger.LogError(message);
        throw new Exception(message);
    }

    #endregion


    // ============================================================
    // 1) CITY AVAILABILITY
    // ============================================================

    public Task<SnappTripCityAvailabilityResponse> SearchCityAvailabilityAsync(SnappTripCityAvailabilityRequest request)
    {
        return PostAsync<SnappTripCityAvailabilityResponse>("/availability/cities", request);
    }

    // ============================================================
    // 2) HOTEL AVAILABILITY (REAL-TIME)
    // ============================================================

    public Task<IEnumerable<SnappTripRoomAvailability>> GetHotelAvailabilityAsync(long[] hotelIds, string checkIn, string checkOut)
    {
        var ids = string.Join(',', hotelIds.Select(x => x.ToString()));
        var url = $"/availability/hotels?id={ids}&checkin={checkIn}&checkout={checkOut}";

        return GetAsync<IEnumerable<SnappTripRoomAvailability>>(url);
    }

    // ============================================================
    // 3) HOTEL AVAILABILITY (REAL-TIME)
    // ============================================================

    public Task<SnappTripAvailabilityResponse> GetHotelAvailabilityAsync(long hotelId, string checkIn, string checkOut)
    {
        var url = $"/availability/hotels/{hotelId}?&checkin={checkIn}&checkout={checkOut}";
        return GetAsync<SnappTripAvailabilityResponse>(url);
    }

    // ============================================================
    // 7) CREATE BOOKING
    // ============================================================

    public Task<SnappTripBookingCreateResponse> CreateBookingAsync(SnappTripCreateBookingRequest req)
    {
        return PostAsync<SnappTripBookingCreateResponse>("/booking/create", req);
    }

    // ============================================================
    // 8) BOOKING STATUS
    // ============================================================

    public Task<SnappTripBookingStatusResponse> GetBookingStatusAsync(string reservationCode)
    {
        return GetAsync<SnappTripBookingStatusResponse>($"/booking/{reservationCode}");
    }

    // ============================================================
    // 9) BOOKING LOCK
    // ============================================================

    public async Task LockBookingAsync(string reservationCode)
    {
        _logger.LogInformation("SnappTrip POST /booking/{code}/lock", reservationCode);

        var res = await _http.PostAsync($"/booking/{reservationCode}/lock", null);

        if (!res.IsSuccessStatusCode)
            await ThrowApiError(res, $"/booking/{reservationCode}/lock");
    }

    // ============================================================
    // 10) BOOKING CONFIRM
    // ============================================================

    public Task<SnappTripBookingStatusResponse> ConfirmBookingAsync(string reservationCode)
    {
        return PostAsync<SnappTripBookingStatusResponse>(
            $"/booking/{reservationCode}/confirm",
            new { }  // body خالی
        );
    }




    // ============================================================
    // 14) HOTEL DETAILS (STATIC)
    // ============================================================

    public Task<SnappTripHotelDetailsResponse> GetHotelDetailsAsync(long hotelId)
    {
        return GetAsync<SnappTripHotelDetailsResponse>($"/hotels/?id={hotelId}");
    }

    // ============================================================
    // 15) HOTEL FACILITIES (STATIC)
    // ============================================================

    public Task<SnappTripHotelFacilitiesResponse> GetHotelFacilitiesAsync(long hotelId)
    {
        return GetAsync<SnappTripHotelFacilitiesResponse>($"/hotels/facilities?id={hotelId}");
    }

    // ============================================================
    // 16) HOTEL GALLERIES (STATIC)
    // ============================================================

    public Task<IEnumerable<SnappTripHotelGalleriesResponse>> GetHotelGalleriesAsync(long hotelId)
    {
        return GetAsync<IEnumerable<SnappTripHotelGalleriesResponse>>($"/hotels/galleries?id={hotelId}");
    }

    // ============================================================
    // 17) HOTEL ROOMS (STATIC)
    // ============================================================

    public Task<SnappTripHotelRoomsResponse> GetHotelRoomsAsync(long hotelId)
    {
        return GetAsync<SnappTripHotelRoomsResponse>($"/hotels/rooms?id={hotelId}");
    }



}
