using Offgrid.Framework.Domain;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Events;
using Offgrid.Portal.Customers.Domain.Repositories;
using Offgrid.Portal.Customers.Domain.Services;

namespace Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;

public interface IReinstateCustomerCommandHandler
{
    Task<ReinstateCustomerResult> HandleAsync(
        Guid customerId,
        ReinstateCustomerCommand command,
        CancellationToken cancellationToken = default);
}

public sealed class ReinstateCustomerCommandHandler : IReinstateCustomerCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerChangeFactory _customerChangeFactory;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly TimeProvider _timeProvider;

    public ReinstateCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        ICustomerChangeFactory customerChangeFactory,
        IDomainEventDispatcher domainEventDispatcher,
        TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
        ArgumentNullException.ThrowIfNull(customerChangeFactory, nameof(customerChangeFactory));
        ArgumentNullException.ThrowIfNull(domainEventDispatcher, nameof(domainEventDispatcher));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _unitOfWork = unitOfWork;
        _customerChangeFactory = customerChangeFactory;
        _domainEventDispatcher = domainEventDispatcher;
        _timeProvider = timeProvider;
    }

    public async Task<ReinstateCustomerResult> HandleAsync(
        Guid customerId,
        ReinstateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var isValid = command.TryValidate(out var validationErrors);
        if (!isValid)
        {
            throw new ValidationException("Invalid reinstate customer command", validationErrors);
        }

        var existingCustomer = await _unitOfWork.Customers
            .GetByIdAsync(customerId, cancellationToken)
            ?? throw new EntityNotFoundException($"Customer with ID {customerId} not found.")
            {
                EntityKey = customerId.ToString(),
                EntityType = nameof(Customer)
            };

        var (reason, reinstatedBy) = command;
        existingCustomer.Reinstate(reinstatedBy, reason, _timeProvider);

        var domainEvents = existingCustomer.DomainEvents.ToList();
        RecordCustomerChangedEvents(domainEvents);

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

        if (domainEvents.Count > 0)
        {
            await _domainEventDispatcher.DispatchEventsAsync(domainEvents, cancellationToken);
        }

        return new ReinstateCustomerResult(existingCustomer.CustomerId, existingCustomer.Status.ToString());
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
}
