using Refahi.Contract.Interfaces;
using Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.ValueObjects;

namespace Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.DomainEvents;

public sealed class ProvisionalBookingCreatedEvent : IDomainEvent
{
    public BookingId BookingId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public ProvisionalBookingCreatedEvent(BookingId bookingId)
    {
        BookingId = bookingId;
    }
}
