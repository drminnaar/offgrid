using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

namespace Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;

public interface IChangeCustomerStatusCommandHandler
{
    Task<ChangeCustomerStatusResult> HandleAsync(
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

    public async Task<ChangeCustomerStatusResult> HandleAsync(
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
                EntityType = nameof(Customer)
            };
        }

        if (!Enum.TryParse<CustomerStatus>(command.Status, ignoreCase: true, out var newStatus))
        {
            throw new ValidationException(
                $"Invalid status value: {command.Status}",
                new ValidationException.ValidationError(nameof(command.Status), $"Status must be a valid value of {string.Join(", ", Enum.GetNames<CustomerStatus>())}."));
        }

        existingCustomer.ChangeStatus(newStatus, _timeProvider);
        await _customerRepository.SaveChangesAsync(cancellationToken);
        return existingCustomer.ToChangeCustomerStatusResult();
    }
}
