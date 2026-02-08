using Microsoft.EntityFrameworkCore;
using Offgrid.Framework.Domain;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Domain.Entities;
using Offgrid.Portal.Customers.Domain.Repositories;

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
    private readonly ICustomerRepository _customerRepository;
    private readonly TimeProvider _timeProvider;

    public ReinstateCustomerCommandHandler(ICustomerRepository customerRepository, TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(customerRepository, nameof(customerRepository));
        ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
        _customerRepository = customerRepository;
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

        var existingCustomer = await _customerRepository
            .GetByIdAsync(customerId, cancellationToken)
            ?? throw new EntityNotFoundException($"Customer with ID {customerId} not found.")
            {
                EntityKey = customerId.ToString(),
                EntityType = nameof(Customer)
            };

        var (reason, reinstatedBy) = command;
        existingCustomer.Reinstate(reinstatedBy, reason, _timeProvider);
        _customerRepository.Update(existingCustomer);
        await _customerRepository.SaveChangesAsync(cancellationToken);

        return new ReinstateCustomerResult(existingCustomer.CustomerId, existingCustomer.Status.ToString());
    }
}
