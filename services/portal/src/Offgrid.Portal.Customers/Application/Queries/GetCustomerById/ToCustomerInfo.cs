using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Application.Queries.GetCustomerById;

public static partial class GetCustomerByIdExtensions
{
    public static CustomerDetail ToCustomerDetail(this Customer customer)
    {
        return new CustomerDetail
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
