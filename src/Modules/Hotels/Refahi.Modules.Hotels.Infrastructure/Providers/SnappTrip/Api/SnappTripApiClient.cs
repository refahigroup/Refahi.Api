using Microsoft.Extensions.Logging;
using Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Contracts;
using System.Net.Http.Json;

namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Api
{
    public class SnappTripApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<SnappTripApiClient> _logger;

        public SnappTripApiClient(HttpClient client, ILogger<SnappTripApiClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            _logger.LogInformation("SnappTrip GET {Url}", url);

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result == null)
                throw new Exception("Invalid empty response from SnappTrip API.");

            return result;
        }

        private async Task<T> PostAsync<T>(string url, object payload)
        {
            _logger.LogInformation("SnappTrip POST {Url}", url);

            var response = await _client.PostAsJsonAsync(url, payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result == null)
                throw new Exception("Invalid empty response from SnappTrip API.");

            return result;
        }

        public Task<SnappTripAvailabilityResponse> SearchHotelsAsync(int cityId, DateOnly checkIn, DateOnly checkOut)
        {
            var url = $"/availability/cities?city_id={cityId}&checkin={checkIn:yyyy-MM-dd}&checkout={checkOut:yyyy-MM-dd}";
            return GetAsync<SnappTripAvailabilityResponse>(url);
        }

        public Task<SnappTripHotelDetailsResponse> GetHotelDetailsAsync(long hotelId)
        {
            var url = $"/hotels?hotel_id={hotelId}";
            return GetAsync<SnappTripHotelDetailsResponse>(url);
        }

        public Task<SnappTripBookingCreateResponse> CreateBookingAsync(object body)
        {
            return PostAsync<SnappTripBookingCreateResponse>("/booking/create", body);
        }

        public Task<SnappTripBookingStatusResponse> GetBookingStatusAsync(string code)
        {
            var url = $"/booking/{code}";
            return GetAsync<SnappTripBookingStatusResponse>(url);
        }

        public async Task LockBookingAsync(string code)
        {
            _logger.LogInformation("SnappTrip LOCK {Code}", code);
            var response = await _client.PostAsync($"/booking/{code}/lock", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task ConfirmBookingAsync(string code)
        {
            _logger.LogInformation("SnappTrip CONFIRM {Code}", code);
            var response = await _client.PostAsync($"/booking/{code}/confirm", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
