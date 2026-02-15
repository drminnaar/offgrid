using Microsoft.AspNetCore.Mvc;

namespace Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;

public sealed class GetAllCustomersQuery
{
    public const int DefaultPageSize = 10;
    public const int DefaultPageNumber = 1;

    [FromQuery(Name = "limit")]
    public int PageSize { get; init; } = DefaultPageSize;

    [FromQuery(Name = "page")]
    public int PageNumber { get; init; } = DefaultPageNumber;
}
