using Offgrid.Customers.Application.Commands.ChangeCustomerStatus;

namespace Offgrid.Customers.Application.Services;

public interface ICustomerService
{
    Task UpsertCustomerAsync(
        Guid customerId,
        ChangeCustomerStatusCommand command,
        CancellationToken cancellationToken = default);
}

public sealed class CustomerService : ICustomerService
{
    private readonly IChangeCustomerStatusCommandHandler _handler;

    public CustomerService(IChangeCustomerStatusCommandHandler handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public async Task UpsertCustomerAsync(
        Guid customerId,
        ChangeCustomerStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        await _handler.HandleAsync(customerId, command, cancellationToken);
    }
}
