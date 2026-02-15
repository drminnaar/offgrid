using Offgrid.Framework.Domain;
using Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;
using Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;
using Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;
using Offgrid.Portal.Customers.Application.Queries.GetCustomerById;

namespace Offgrid.Portal.Customers.Application.Services;

public interface ICustomerService
{
    Task<CustomerDetail> GetCustomerByIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    Task<PagedListResult<CustomerInfo>> GetAllCustomersAsync(
        GetAllCustomersQuery query,
        CancellationToken cancellationToken = default);

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
    private readonly IGetAllCustomersQueryHandler _getAllCustomersQueryHandler;
    private readonly IGetCustomerByIdQueryHandler _getCustomerByIdQueryHandler;

    public CustomerService(
        IReinstateCustomerCommandHandler reinstateHandler,
        ISuspendCustomerCommandHandler suspendHandler,
        IGetAllCustomersQueryHandler getAllCustomersQueryHandler,
        IGetCustomerByIdQueryHandler getCustomerByIdQueryHandler)
    {
        ArgumentNullException.ThrowIfNull(reinstateHandler, nameof(reinstateHandler));
        ArgumentNullException.ThrowIfNull(suspendHandler, nameof(suspendHandler));
        ArgumentNullException.ThrowIfNull(getAllCustomersQueryHandler, nameof(getAllCustomersQueryHandler));
        ArgumentNullException.ThrowIfNull(getCustomerByIdQueryHandler, nameof(getCustomerByIdQueryHandler));
        _reinstateHandler = reinstateHandler;
        _suspendHandler = suspendHandler;
        _getAllCustomersQueryHandler = getAllCustomersQueryHandler;
        _getCustomerByIdQueryHandler = getCustomerByIdQueryHandler;
    }

    public async Task<PagedListResult<CustomerInfo>> GetAllCustomersAsync(
        GetAllCustomersQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _getAllCustomersQueryHandler.Handle(query, cancellationToken);
    }

    public async Task<CustomerDetail> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _getCustomerByIdQueryHandler.Handle(customerId, cancellationToken);
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
