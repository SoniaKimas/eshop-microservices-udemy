using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification // allwow to use mediatR handlers
{
    Guid EventId => Guid.NewGuid();

    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
