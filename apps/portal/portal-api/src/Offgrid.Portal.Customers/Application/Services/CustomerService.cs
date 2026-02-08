using Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;
using Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;

namespace Offgrid.Portal.Customers.Application.Services;

public interface ICustomerService
{
    Task<ReinstateCustomerResult> ReinstateCustomerAsync(
        Guid customerId,
        ReinstateCustomerCommand command,
        CancellationToken cancellationToken = default);

    Task<SuspendCustomerResult> SuspendCustomerAsync(
        Guid customerId,
        SuspendCustomerCommand command,
        CancellationToken cancellationToken = default);
}

public sealed class CustomerService : ICustomerService
{
    private readonly IReinstateCustomerCommandHandler _reinstateHandler;
    private readonly ISuspendCustomerCommandHandler _suspendHandler;

    public CustomerService(IReinstateCustomerCommandHandler reinstateHandler, ISuspendCustomerCommandHandler suspendHandler)
    {
        _reinstateHandler = reinstateHandler ?? throw new ArgumentNullException(nameof(reinstateHandler));
        _suspendHandler = suspendHandler ?? throw new ArgumentNullException(nameof(suspendHandler));
    }

    public async Task<ReinstateCustomerResult> ReinstateCustomerAsync(
        Guid customerId,
        ReinstateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        return await _reinstateHandler.HandleAsync(customerId, command, cancellationToken);
    }

    public async Task<SuspendCustomerResult> SuspendCustomerAsync(
        Guid customerId,
        SuspendCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        return await _suspendHandler.HandleAsync(customerId, command, cancellationToken);
    }
}
