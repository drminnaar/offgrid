using Microsoft.AspNetCore.Mvc;
using Offgrid.Customers.Application.Commands.UpsertCustomer;
using Offgrid.Customers.Application.Services;

namespace Offgrid.ShopApi.Endpoints.Customers;

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
