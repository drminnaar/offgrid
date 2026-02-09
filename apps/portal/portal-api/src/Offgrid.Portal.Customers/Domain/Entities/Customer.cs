using System.ComponentModel.DataAnnotations;
using Offgrid.Framework.Domain;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Events;

namespace Offgrid.Portal.Customers.Domain.Entities;

public sealed class Customer : AggregateRoot
{
    public Guid CustomerId { get; private set; }
    public string CustomerNumber { get; private set; } = string.Empty;
    public string KeycloakUserId { get; private set; } = string.Empty;
    public CustomerStatus Status { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset? UpdatedDate { get; private set; }
    public DateTimeOffset? DeletedDate { get; private set; }

    [Timestamp]  // EF Core will manage this automatically
    public byte[] Version { get; private set; } = [];

    private Customer() : base()
    {
    }

    public void Reinstate(string reinstatedBy, string reason, TimeProvider timeProvider)
    {
        if (string.IsNullOrWhiteSpace(reinstatedBy))
        {
            throw new DomainException("Reinstated by is required.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new DomainException("Reinstated reason is required.");
        }

        var currentStatus = Status;
        var newStatus = CustomerStatus.Active;

        if (currentStatus == newStatus)
        {
            return;
        }

        if (currentStatus != CustomerStatus.Suspended)
        {
            throw new DomainException("Only suspended customers can be reinstated.");
        }

        if (IsDeleted)
        {
            throw new DomainException("Deleted customers cannot be reinstated.");
        }

        var reinstatedDate = timeProvider.GetUtcNow();
        Status = CustomerStatus.Active;
        UpdatedDate = reinstatedDate;

        RaiseDomainEvent(new CustomerReinstatedEvent(CustomerId, reinstatedDate, reason));
        RaiseDomainEvent(new CustomerChangedEvent(CustomerId, reinstatedDate, new(
            reinstatedBy,
            [
                new Change(nameof(Status), currentStatus.ToString(), newStatus.ToString(), [reason])
            ]
        )));
    }

    public void Suspend(string suspendedBy, string reason, TimeProvider timeProvider)
    {
        if (string.IsNullOrWhiteSpace(suspendedBy))
        {
            throw new DomainException("Suspended by is required.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new DomainException("Suspension reason is required.");
        }

        var currentStatus = Status;
        var newStatus = CustomerStatus.Suspended;

        if (currentStatus == newStatus)
        {
            return;
        }

        if (currentStatus != CustomerStatus.Active)
        {
            throw new DomainException("Only active customers can be suspended.");
        }

        if (IsDeleted)
        {
            throw new DomainException("Deleted customers cannot be suspended.");
        }

        var suspendedDate = timeProvider.GetUtcNow();
        Status = CustomerStatus.Suspended;
        UpdatedDate = suspendedDate;

        RaiseDomainEvent(new CustomerSuspendedEvent(CustomerId, suspendedDate, reason));
        RaiseDomainEvent(new CustomerChangedEvent(CustomerId, suspendedDate, new(
            suspendedBy,
            [
                new Change(nameof(Status), currentStatus.ToString(), newStatus.ToString(), [reason])
            ]
        )));
    }

    private bool IsDeleted => DeletedDate.HasValue;
}
