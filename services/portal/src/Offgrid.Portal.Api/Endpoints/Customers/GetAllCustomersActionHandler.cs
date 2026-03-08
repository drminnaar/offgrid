using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.Exceptions;
using Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;
using Offgrid.Portal.Customers.Application.Services;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public sealed class GetAllCustomersActionHandler
{
    public const string EndpointName = "GetAllCustomers";

    public static async Task<IResult> GetAllCustomersAsync(
        [FromServices] ICustomerService customerService,
        [FromQuery(Name = "page")] int pageNumber = GetAllCustomersQuery.DefaultPageNumber,
        [FromQuery(Name = "limit")] int pageSize = GetAllCustomersQuery.DefaultPageSize,
        [FromQuery(Name = "status")] string status = "",
        CancellationToken token = default)
    {
        var query = new GetAllCustomersQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
        };

        if (!query.TryValidate(out var errors))
        {
            throw new ValidationException("Invalid query parameters.", errors);
        }

        var result = await customerService.GetAllCustomersAsync(query, token);

        return TypedResults.Ok(result);
    }
}
