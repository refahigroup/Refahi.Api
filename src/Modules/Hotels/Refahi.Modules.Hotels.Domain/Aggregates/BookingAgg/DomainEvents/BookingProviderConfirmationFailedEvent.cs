using Refahi.Contract.Interfaces;
using Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.ValueObjects;

namespace Refahi.Modules.Hotels.Domain.Aggregates.BookingAgg.DomainEvents;

public sealed class BookingProviderConfirmationFailedEvent : IDomainEvent
{
    public BookingId BookingId { get; }
    public string Reason { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public BookingProviderConfirmationFailedEvent(BookingId bookingId, string reason)
    {
        BookingId = bookingId;
        Reason = reason;
    }
}
