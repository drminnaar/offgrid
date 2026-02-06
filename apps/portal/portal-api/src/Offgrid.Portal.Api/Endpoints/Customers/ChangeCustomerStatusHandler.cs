using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;
using Offgrid.Portal.Customers.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public sealed class ChangeCustomerStatusHandler
{
    public const string EndpointName = "ChangeCustomerStatus";

    public static async Task<IResult> ChangeCustomerStatusAsync(
        [FromBody] ChangeCustomerStatusCommand command,
        [FromRoute] Guid customerId,
        [FromServices] ICustomerService customerService,
        CancellationToken token = default)
    {
        var result = await customerService.ChangeCustomerStatusAsync(customerId, command, token);
        return TypedResults.Ok(result);
    }
}
