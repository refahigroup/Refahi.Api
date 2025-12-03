namespace Refahi.Contract.Interfaces;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
