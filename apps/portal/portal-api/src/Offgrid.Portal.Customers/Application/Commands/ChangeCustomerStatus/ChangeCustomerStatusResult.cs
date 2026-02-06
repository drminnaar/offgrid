namespace Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;

public sealed record ChangeCustomerStatusResult
{
    public required Guid CustomerId { get; init; }
    public required string Status { get; init; }
}
