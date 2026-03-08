using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;
using Offgrid.Portal.Customers.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public sealed class ReinstateCustomerActionHandler
{
    public const string EndpointName = "ReinstateCustomer";

    public static async Task<IResult> ReinstateCustomerAsync(
        [FromRoute] Guid customerId,
        [FromBody] ReinstateCustomerCommand command,
        [FromServices] ICustomerService customerService,
        HttpContext httpContext,
        CancellationToken token = default)
    {
        var username = httpContext.RequiredUsername();

        var result = await customerService.ReinstateCustomerAsync(
            customerId,
            command with { ReinstatedBy = username },
            token);

        return TypedResults.Ok(result);
    }
}
