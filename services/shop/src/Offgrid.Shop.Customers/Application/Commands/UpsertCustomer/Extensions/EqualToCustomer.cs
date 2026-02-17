using Offgrid.Shop.Customers.Domain.Entities;

namespace Offgrid.Shop.Customers.Application.Commands.UpsertCustomer.Extensions;

public static partial class Extensions
{
    public static bool EqualToCustomer(this UpsertCustomerCommand command, Customer customer) =>
        customer.Email.Equals(command.Email, StringComparison.OrdinalIgnoreCase) &&
        customer.FullName.Equals(command.FullName, StringComparison.Ordinal) &&
        customer.KeycloakUserId.Equals(command.KeycloakUserId, StringComparison.Ordinal);
}
