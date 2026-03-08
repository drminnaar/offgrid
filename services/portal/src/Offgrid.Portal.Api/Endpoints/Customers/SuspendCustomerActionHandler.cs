using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;
using Offgrid.Portal.Customers.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public sealed class SuspendCustomerActionHandler
{
    public const string EndpointName = "SuspendCustomer";

    public static async Task<IResult> SuspendCustomerAsync(
        [FromRoute] Guid customerId,
        [FromBody] SuspendCustomerCommand command,
        [FromServices] ICustomerService customerService,
        HttpContext httpContext,
        CancellationToken token = default)
    {
        var username = httpContext.RequiredUsername();

        var result = await customerService.SuspendCustomerAsync(
            customerId,
            command with { SuspendedBy = username },
            token);

        return TypedResults.Ok(result);
    }
}
