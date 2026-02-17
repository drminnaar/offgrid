using Offgrid.Shop.Customers.Domain.Entities;

namespace Offgrid.Shop.Customers.Application.Commands.UpsertCustomer.Extensions;

public static partial class Extensions
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
