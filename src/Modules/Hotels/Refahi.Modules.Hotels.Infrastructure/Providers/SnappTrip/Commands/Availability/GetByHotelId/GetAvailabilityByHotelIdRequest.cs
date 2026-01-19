namespace Refahi.Modules.Hotels.Infrastructure.Providers.SnappTrip.Commands.Availability.GetByHotelId;

public record GetAvailabilityByHotelIdRequest
(
    int HotelId,
    DateOnly Checkin,
    DateOnly Checkout
);
