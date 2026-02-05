using Offgrid.Customers.Domain.Exceptions;
using Offgrid.Customers.Domain.Repositories;

namespace Offgrid.Customers.Application.Commands.ChangeCustomerStatus;

public interface IChangeCustomerStatusCommandHandler
{
    Task HandleAsync(
        Guid customerId,
        ChangeCustomerStatusCommand command,
        CancellationToken cancellationToken = default);
}

public sealed class ChangeCustomerStatusCommandHandler : IChangeCustomerStatusCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly TimeProvider _timeProvider;

    public ChangeCustomerStatusCommandHandler(
        ICustomerRepository customerRepository,
        TimeProvider timeProvider)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async Task HandleAsync(
        Guid customerId,
        ChangeCustomerStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var isValid = command.TryValidate(out var validationErrors);
        if (!isValid)
        {
            throw new ValidationException("Invalid customer status", validationErrors);
        }

        var existingCustomer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);

        if (existingCustomer == null)
        {
            throw new EntityNotFoundException($"Customer with ID {customerId} not found.")
            {
                EntityKey = customerId.ToString(),
                EntityType = nameof(Domain.Entities.Customer)
            };
        }

        existingCustomer.ChangeStatus(command.Status, _timeProvider);
        await _customerRepository.SaveChangesAsync(cancellationToken);
    }
}
