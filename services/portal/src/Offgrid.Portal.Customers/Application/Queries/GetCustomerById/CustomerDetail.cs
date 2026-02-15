namespace Offgrid.Portal.Customers.Application.Queries.GetCustomerById;

public sealed record CustomerDetail
{
    public Guid CustomerId { get; init; }
    public string CustomerNumber { get; init; } = string.Empty;
    public string KeycloakUserId { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTimeOffset CreatedDate { get; init; }
    public DateTimeOffset? UpdatedDate { get; init; }
    public DateTimeOffset? DeletedDate { get; init; }

}
