using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;
using Offgrid.Portal.Customers.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public sealed class GetAllCustomersActionHandler
{
    public const string EndpointName = "GetAllCustomers";

    public static async Task<IResult> GetAllCustomersAsync(
        [FromServices] ICustomerService customerService,
        HttpContext httpContext,
        [FromQuery(Name = "page")] int pageNumber = GetAllCustomersQuery.DefaultPageNumber,
        [FromQuery(Name = "limit")] int pageSize = GetAllCustomersQuery.DefaultPageSize,
        CancellationToken token = default)
    {
        var _ = httpContext
            .Username()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var query = new GetAllCustomersQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await customerService.GetAllCustomersAsync(query, token);

        return TypedResults.Ok(result);
    }
}
