namespace Offgrid.Customers.Application.Commands.UpsertCustomer;

public class UpsertCustomerResult
{
    public required Guid CustomerId { get; init; }
    public required string KeycloakUserId { get; set; }
    public required string CustomerNumber { get; init; }
    public required string Status { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required long CreatedDateUnixTimeSeconds { get; init; }
    public long? UpdatedDateUnixTimeSeconds { get; init; }
}
