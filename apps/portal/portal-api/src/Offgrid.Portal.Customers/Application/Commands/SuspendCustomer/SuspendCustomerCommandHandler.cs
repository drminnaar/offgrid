using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

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
    private readonly ICustomerRepository _customerRepository;
    private readonly TimeProvider _timeProvider;

    public SuspendCustomerCommandHandler(ICustomerRepository customerRepository, TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(customerRepository, nameof(customerRepository));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _customerRepository = customerRepository;
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
            throw new ValidationException("Invalid suspend customer command", validationErrors);
        }

        var existingCustomer = await _customerRepository
            .GetByIdAsync(customerId, cancellationToken)
            ?? throw new EntityNotFoundException($"Customer with ID {customerId} not found.")
            {
                EntityKey = customerId.ToString(),
                EntityType = nameof(Customer)
            };

        var (reason, suspendedBy) = command;
        existingCustomer.Suspend(suspendedBy, reason, _timeProvider);
        _customerRepository.Update(existingCustomer);
        await _customerRepository.SaveChangesAsync(cancellationToken);

        return new SuspendCustomerResult(existingCustomer.CustomerId, existingCustomer.Status.ToString());
    }
}
