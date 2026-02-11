namespace Offgrid.Framework.Domain;

public abstract class AggregateRoot
{
    protected AggregateRoot()
    {
    }

    private readonly List<IDomainEvent> _domainEvents = [];

    // Returning _domainEvents.ToArray() gives a stable snapshot:
    //  - Enumeration is safe and consistent even if you add events later.
    //  - The consumer sees exactly what existed at the time of access.
    //  - Slight allocation cost, but predictable behavior.
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToArray();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RestoreDomainEvents(IEnumerable<IDomainEvent> domainEvents)
    {
        ArgumentNullException.ThrowIfNull(domainEvents);
        _domainEvents.AddRange(domainEvents);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
