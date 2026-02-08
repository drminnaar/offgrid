namespace Offgrid.Framework.Domain;

public interface IDomainEvent
{
    public string EventId { get; }
    public string EventName { get; }
    public string CorrelationId { get; }
    public DateTimeOffset OccurredAt { get; }
}
