namespace Offgrid.Framework.Domain;

public interface IDomainEvent
{
    public string EventTypeId { get; }
    public string EventType { get; }
    public string CorrelationId { get; }
    public string AggregateId { get; }
    public DateTimeOffset OccurredAt { get; }
}
