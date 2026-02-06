using Microsoft.AspNetCore.Mvc;
using Offgrid.Shop.Customers.Application.Commands.UpsertCustomer;
using Offgrid.Shop.Customers.Application.Services;

namespace Offgrid.Shop.Api.Endpoints.Customers;

public sealed class UpsertCustomerHandler
{
    public const string EndpointName = "UpsertCustomer";

    public static async Task<IResult> UpsertCustomerAsync(
        [FromBody] UpsertCustomerCommand customer,
        [FromServices] ICustomerService customerService,
        CancellationToken token = default)
    {
        var result = await customerService.UpsertCustomerAsync(customer, token);
        return TypedResults.Ok(result);
    }
}
