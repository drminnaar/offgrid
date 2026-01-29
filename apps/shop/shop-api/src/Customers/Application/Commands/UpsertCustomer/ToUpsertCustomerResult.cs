using Offgrid.Customers.Domain.Entities;

namespace Offgrid.Customers.Application.Commands.UpsertCustomer;

public static partial class MapExtensions
{
    public static UpsertCustomerResult ToUpsertCustomerResult(this Customer customer)
    {
        return new UpsertCustomerResult
        {
            CustomerId = customer.CustomerId,
            CustomerNumber = customer.CustomerNumber,
            KeycloakUserId = customer.KeycloakUserId,
            Status = customer.Status.ToString(),
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            CreatedDateUnixTimeSeconds = customer.CreatedDate.ToUnixTimeSeconds(),
            Email = customer.Email,
            UpdatedDateUnixTimeSeconds = customer.UpdatedDate?.ToUnixTimeSeconds()
        };
    }
}
