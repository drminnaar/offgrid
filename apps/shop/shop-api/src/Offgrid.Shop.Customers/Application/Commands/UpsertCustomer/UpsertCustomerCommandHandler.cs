using Offgrid.Framework.Exceptions;
using Offgrid.Shop.Customers.Domain.Repositories;
using Offgrid.Shop.Customers.Domain.Services;

namespace Offgrid.Shop.Customers.Application.Commands.UpsertCustomer;

public interface IUpsertCustomerCommandHandler
{
    Task<UpsertCustomerResult> HandleAsync(UpsertCustomerCommand command, CancellationToken cancellationToken = default);
}

public sealed class UpsertCustomerCommandHandler : IUpsertCustomerCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerFactory _customerFactory;
    private readonly TimeProvider _timeProvider;

    public UpsertCustomerCommandHandler(
        ICustomerRepository customerRepository,
        ICustomerFactory customerFactory,
        TimeProvider timeProvider)
    {
        _customerFactory = customerFactory ?? throw new ArgumentNullException(nameof(customerFactory));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public async Task<UpsertCustomerResult> HandleAsync(UpsertCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var isValid = command.TryValidate(out var validationErrors);
        if (!isValid)
        {
            throw new ValidationException("Invalid customer details", validationErrors);
        }

        var existingCustomer = await _customerRepository.GetByKeycloakUserIdAsync(command.KeycloakUserId, cancellationToken);

        var (email, fullName, keycloakUserId) = command;
        if (existingCustomer != null)
        {
            existingCustomer.Update(fullName, email, _timeProvider);
            await _customerRepository.UpdateAsync(existingCustomer, cancellationToken);
            return existingCustomer.ToUpsertCustomerResult();
        }
        else
        {
            var newCustomer = _customerFactory.Create(
                keycloakUserId,
                email,
                fullName);
            await _customerRepository.AddAsync(newCustomer, cancellationToken);
            return newCustomer.ToUpsertCustomerResult();
        }
    }
}
