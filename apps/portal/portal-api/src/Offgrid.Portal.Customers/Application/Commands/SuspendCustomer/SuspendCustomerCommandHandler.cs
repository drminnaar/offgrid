using Offgrid.Customers.Contracts.DomainEvents;
using Offgrid.Framework.Domain;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Events;
using Offgrid.Portal.Customers.Domain.Repositories;
using Offgrid.Portal.Customers.Domain.Services;

namespace Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;

public interface ISuspendCustomerCommandHandler
{
    Task<SuspendCustomerResult> HandleAsync(
        Guid customerId,
        SuspendCustomerCommand command,
        CancellationToken cancellationToken = default);
}

public sealed class SuspendCustomerCommandHandler : ISuspendCustomerCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerChangeFactory _customerChangeFactory;
    private readonly ICustomerOutboxFactory _customerOutboxFactory;
    private readonly TimeProvider _timeProvider;

    public SuspendCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerChangeFactory customerChangeFactory,
        ICustomerOutboxFactory customerOutboxFactory,
        TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
        ArgumentNullException.ThrowIfNull(customerChangeFactory, nameof(customerChangeFactory));
        ArgumentNullException.ThrowIfNull(customerOutboxFactory, nameof(customerOutboxFactory));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _unitOfWork = unitOfWork;
        _customerChangeFactory = customerChangeFactory;
        _customerOutboxFactory = customerOutboxFactory;
        _timeProvider = timeProvider;
    }

    public async Task<SuspendCustomerResult> HandleAsync(
        Guid customerId,
        SuspendCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var isValid = command.TryValidate(out var validationErrors);
        if (!isValid)
        {
            throw new ValidationException("Invalid request to suspend customer.", validationErrors);
        }

        var existingCustomer = await _unitOfWork.Customers
            .GetByIdAsync(customerId, cancellationToken)
            ?? throw new EntityNotFoundException($"Customer with ID {customerId} not found.")
            {
                EntityKey = customerId.ToString(),
                EntityType = nameof(Customer)
            };

        var (reason, suspendedBy) = command;
        existingCustomer.Suspend(suspendedBy, reason, _timeProvider);

        var domainEvents = existingCustomer.DomainEvents.ToList();
        RecordCustomerChangedEvents(domainEvents);
        RecordOutboxEvents(domainEvents);
        existingCustomer.ClearDomainEvents();
        _unitOfWork.Customers.Update(existingCustomer);

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            existingCustomer.RestoreDomainEvents(domainEvents);
            throw;
        }

        return new SuspendCustomerResult(existingCustomer.CustomerId, existingCustomer.Status.ToString());
    }

    private void RecordCustomerChangedEvents(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        var changeEvents = domainEvents.OfType<CustomerChangedEvent>().ToList();

        if (changeEvents.Count == 0)
        {
            return;
        }

        foreach (var changeEvent in changeEvents)
        {
            var customerChange = _customerChangeFactory.Create(
                customerId: changeEvent.CustomerId,
                changedBy: changeEvent.ChangedBy,
                changes: changeEvent.Changes,
                changedDate: changeEvent.OccurredAt);

            _unitOfWork.CustomerChanges.Add(customerChange);
        }
    }

    private void RecordOutboxEvents(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        if (domainEvents.Count == 0)
        {
            return;
        }

        foreach (var changeEvent in domainEvents)
        {
            var outboxEvent = _customerOutboxFactory.Create(changeEvent);
            _unitOfWork.CustomerOutboxes.Add(outboxEvent);
        }
    }
}
