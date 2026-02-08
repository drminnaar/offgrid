using Offgrid.Framework.Domain;

namespace Offgrid.Portal.Customers.Domain.Entities;

public sealed class CustomerChange
{
    public Guid CustomerChangeId { get; private set; }
    public Guid CustomerId { get; private set; }
    public string ChangedBy { get; private set; } = string.Empty;
    public DateTimeOffset ChangedAt { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public List<Change> Changes { get; private set; } = [];

    private CustomerChange() : base()
    {
    }

    public static CustomerChange CreateNew(
        Guid customerChangeId,
        Guid customerId,
        string changedBy,
        IReadOnlyCollection<Change> changes,
        DateTimeOffset changedAt,
        DateTimeOffset createdDate)
    {
        return new CustomerChange
        {
            CustomerChangeId = customerChangeId,
            CustomerId = customerId,
            ChangedBy = changedBy,
            Changes = [.. changes],
            ChangedAt = changedAt,
            CreatedDate = createdDate
        };
    }
}
