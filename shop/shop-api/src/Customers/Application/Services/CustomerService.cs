using Offgrid.Customers.Application.Commands.UpsertCustomer;

namespace Offgrid.Customers.Application.Services;

public interface ICustomerService
{
    Task<UpsertCustomerResult> UpsertCustomerAsync(UpsertCustomerCommand command, CancellationToken cancellationToken = default);
}

public sealed class CustomerService : ICustomerService
{
    private readonly IUpsertCustomerCommandHandler _handler;

    public CustomerService(IUpsertCustomerCommandHandler handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public async Task<UpsertCustomerResult> UpsertCustomerAsync(UpsertCustomerCommand command, CancellationToken cancellationToken = default)
    {
        return await _handler.HandleAsync(command, cancellationToken);
    }
}
