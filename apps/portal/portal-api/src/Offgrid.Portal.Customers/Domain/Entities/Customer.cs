
using System.ComponentModel.DataAnnotations;
using Offgrid.Framework.Exceptions;

namespace Offgrid.Portal.Customers.Domain.Entities;

public sealed class Customer
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

    private Customer()
    {
    }

    public void ChangeStatus(CustomerStatus newStatus, TimeProvider timeProvider)
    {
        if (IsDeleted)
        {
            throw new DomainException("Cannot change status of a deleted customer.");
        }

        if (Status == newStatus)
        {
            return;
        }

        switch (newStatus)
        {
            case CustomerStatus.Active:
                Reinstate(timeProvider);
                break;
            case CustomerStatus.Suspended:
                Suspend(timeProvider);
                break;
            default:
                throw new DomainException($"Unsupported customer status: {newStatus}");
        }
    }

    public void Reinstate(TimeProvider timeProvider)
    {
        if (Status != CustomerStatus.Suspended)
        {
            throw new DomainException("Only suspended customers can be reinstated.");
        }

        if (IsDeleted)
        {
            throw new DomainException("Deleted customers cannot be reinstated.");
        }

        Status = CustomerStatus.Active;
        UpdatedDate = timeProvider.GetUtcNow();
    }

    public void Suspend(TimeProvider timeProvider)
    {
        if (Status != CustomerStatus.Active)
        {
            throw new DomainException("Only active customers can be suspended.");
        }

        if (IsDeleted)
        {
            throw new DomainException("Deleted customers cannot be suspended.");
        }

        Status = CustomerStatus.Suspended;
        UpdatedDate = timeProvider.GetUtcNow();
    }

    private bool IsDeleted => DeletedDate.HasValue;
}
