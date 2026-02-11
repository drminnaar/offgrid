namespace Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;

public sealed record SuspendCustomerResult(Guid CustomerId, string Status);
