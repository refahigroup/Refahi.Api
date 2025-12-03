using Refahi.Contract.Interfaces;
using Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.ValueObjects;

namespace Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.DomainEvents;

public sealed class BookingConfirmedEvent : IDomainEvent
{
    public BookingId BookingId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public BookingConfirmedEvent(BookingId bookingId)
    {
        BookingId = bookingId;
    }
}