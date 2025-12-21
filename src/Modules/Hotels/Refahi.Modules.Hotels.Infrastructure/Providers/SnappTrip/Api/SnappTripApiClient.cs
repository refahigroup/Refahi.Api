using Microsoft.Extensions.Logging;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contract;
using System.Net.Http.Json;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api;

public class SnappTripApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<SnappTripApiClient> _logger;

    public SnappTripApiClient(HttpClient http, ILogger<SnappTripApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    // -----------------------------------------
    // INTERNAL HELPERS
    // -----------------------------------------
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

    // ============================================================
    // 1) CITY AVAILABILITY
    // ============================================================

    public Task<SnappTripCityAvailabilityResponse> SearchCityAvailabilityAsync(
        SnappTripCityAvailabilityRequest request)
    {
        return PostAsync<SnappTripCityAvailabilityResponse>("/availability/cities", request);
    }

    // ============================================================
    // 2) HOTEL DETAILS (STATIC)
    // ============================================================

    public Task<SnappTripHotelDetailsResponse> GetHotelDetailsAsync(long hotelId)
    {
        return GetAsync<SnappTripHotelDetailsResponse>($"/hotels/?id={hotelId}");
    }

    // ============================================================
    // 3) HOTEL ROOMS (STATIC)
    // ============================================================

    public Task<SnappTripHotelRoomsResponse> GetHotelRoomsAsync(long hotelId)
    {
        return GetAsync<SnappTripHotelRoomsResponse>($"/hotels/rooms?id={hotelId}");
    }

    // ============================================================
    // 4) HOTEL FACILITIES (STATIC)
    // ============================================================

    public Task<SnappTripHotelFacilitiesResponse> GetHotelFacilitiesAsync(long hotelId)
    {
        return GetAsync<SnappTripHotelFacilitiesResponse>($"/hotels/facilities?id={hotelId}");
    }

    // ============================================================
    // 5) HOTEL GALLERIES (STATIC)
    // ============================================================

    public Task<IEnumerable<SnappTripHotelGalleriesResponse>> GetHotelGalleriesAsync(long hotelId)
    {
        return GetAsync<IEnumerable<SnappTripHotelGalleriesResponse>>($"/hotels/galleries?id={hotelId}");
    }

    // ============================================================
    // 6) HOTEL AVAILABILITY (REAL-TIME)
    // ============================================================

    public Task<SnappTripAvailabilityResponse> GetHotelAvailabilityAsync(
        long hotelId,
        string checkIn,
        string checkOut)
    {
        var url =
            $"/availability/hotels?id={hotelId}&checkin={checkIn}&checkout={checkOut}";
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
}
