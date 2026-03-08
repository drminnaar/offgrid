using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Application.Services;
using static Offgrid.Framework.Exceptions.ValidationException;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public sealed class GetCustomerByIdActionHandler
{
    public const string EndpointName = "GetCustomerById";

    public static async Task<IResult> GetCustomerByIdAsync(
        [FromServices] ICustomerService customerService,
        [FromRoute] string customerId,
        CancellationToken token = default)
    {
        if (!Guid.TryParse(customerId, out var customerGuid))
        {
            throw new ValidationException(
                $"Invalid customer ID: {customerId}",
                new ValidationError(nameof(customerId), "Invalid GUID format"));
        }

        var result = await customerService.GetCustomerByIdAsync(customerGuid, token);

        return TypedResults.Ok(result);
    }
}
