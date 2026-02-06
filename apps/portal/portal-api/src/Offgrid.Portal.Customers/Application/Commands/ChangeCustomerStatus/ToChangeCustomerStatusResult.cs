using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;

public static partial class Extensions
{
    public static ChangeCustomerStatusResult ToChangeCustomerStatusResult(this Customer customer)
    {
        return new ChangeCustomerStatusResult
        {
            CustomerId = customer.CustomerId,
            Status = customer.Status.ToString()
        };
    }
}
