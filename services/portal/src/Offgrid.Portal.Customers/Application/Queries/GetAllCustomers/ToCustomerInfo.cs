using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;

public static partial class GetAllCustomersExtensions
{
    public static CustomerInfo ToCustomerInfo(this Customer customer)
    {
        return new CustomerInfo
        {
            CustomerId = customer.CustomerId,
            CustomerNumber = customer.CustomerNumber,
            KeycloakUserId = customer.KeycloakUserId,
            Status = customer.Status.ToString(),
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            CreatedDate = customer.CreatedDate,
            UpdatedDate = customer.UpdatedDate,
            DeletedDate = customer.DeletedDate
        };
    }
}
