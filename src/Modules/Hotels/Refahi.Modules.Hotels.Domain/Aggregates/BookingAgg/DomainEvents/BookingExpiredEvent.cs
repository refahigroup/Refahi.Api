using Refahi.Contract.Interfaces;
using Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.ValueObjects;

namespace Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.DomainEvents;

public sealed class BookingExpiredEvent : IDomainEvent
{
    public BookingId BookingId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public BookingExpiredEvent(BookingId bookingId)
    {
        BookingId = bookingId;
    }
}